using MISA.WebFresher05.Application;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Xử lý file excel của tài sản
    /// </summary>
    /// Created by: vtahoang (05/08/2023) 
    public class FixedAssetExcelHandler : BaseExcelHandler<FixedAssetDto, FixedAssetCreateDto>, IFixedAssetExcelHandler
    {
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;

        public FixedAssetExcelHandler(IDepartmentRepository departmentRepository, IFixedAssetCategoryRepository fixedAssetCategoryRepository, IFixedAssetRepository fixedAssetRepository)
        {
            _departmentRepository = departmentRepository;
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

        /// <summary>
        /// File excel mẫu nhập liệu
        /// </summary>
        /// <param name="entityDto">Dữ liệu mẫu</param>
        /// <param name="excelOptions">Định dạng file excel</param>
        /// <returns>File mẫu</returns>
        /// Created by: vtahoang (05/08/2023) 
        public async override Task<byte[]> ExampleFileExcel(FixedAssetDto fixedAssetDto, ExcelOptions excelOptions)
        {
            var fixedAssetDtos = new List<FixedAssetDto> { fixedAssetDto };

            var fileBytes = await ExportToExcel(fixedAssetDtos, excelOptions);

            return fileBytes;
        }

        /// <summary>
        /// Chuyển dữ liệu file excel thành list CreateDto
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        public async Task<IEnumerable<FixedAssetCreateDto>> GetDataFromExcel(byte[] fileBytes)
        {
            var fixedAssetCreateDtos = new List<FixedAssetCreateDto>();

            var departmentList = await _departmentRepository.GetAllAsync();
            var fixedAssetCategoryList = await _fixedAssetCategoryRepository.GetAllAsync();

            using var stream = new MemoryStream(fileBytes);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet != null)
            {
                var errorRows = new List<int>();
                var rowCount = worksheet.Dimension.Rows;
                for (var row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var fixedAssetCreateDto = new FixedAssetCreateDto
                        {
                            fixed_asset_code = worksheet.Cells[row, 2].Value.ToString()?.Trim(),
                            fixed_asset_name = worksheet.Cells[row, 3].Value.ToString()?.Trim(),
                            department_code = worksheet.Cells[row, 4].Value.ToString()?.Trim(),
                            fixed_asset_category_code = worksheet.Cells[row, 5].Value.ToString(),
                            purchase_date = DateTime.FromOADate(Double.Parse(worksheet.Cells[row, 6].Value.ToString()?.Trim() ?? "")),
                            start_using_date = DateTime.FromOADate(Double.Parse(worksheet.Cells[row, 7].Value.ToString()?.Trim() ?? "")),
                            cost = decimal.Parse(worksheet.Cells[row, 8].Value.ToString()?.Trim() ?? ""),
                            quantity = int.Parse(worksheet.Cells[row, 9].Value.ToString()?.Trim() ?? ""),
                            life_time = int.Parse(worksheet.Cells[row, 10].Value.ToString()?.Trim() ?? ""),
                        };

                        // Cập nhật id và tên phòng ban
                        var department = departmentList.FirstOrDefault(x => x.department_code == fixedAssetCreateDto.department_code);
                        if (department != null)
                        {
                            fixedAssetCreateDto.department_id = department.department_id;
                            fixedAssetCreateDto.department_name = department.department_name;
                        }
                        else
                        {
                            throw new Domain.InvalidDataException(String.Format(ResourcesVI.NotExitsDeparmentCode, fixedAssetCreateDto.department_code));
                        }

                        // Cập nhật id và tên loại tài sản
                        var fixedAssetCategory = fixedAssetCategoryList.FirstOrDefault(x => x.fixed_asset_category_code == fixedAssetCreateDto.fixed_asset_category_code);
                        if (fixedAssetCategory != null)
                        {
                            fixedAssetCreateDto.fixed_asset_category_id = fixedAssetCategory.fixed_asset_category_id;
                            fixedAssetCreateDto.fixed_asset_category_name = fixedAssetCategory.fixed_asset_category_name;
                        }
                        else
                        {
                            throw new Domain.InvalidDataException(String.Format(ResourcesVI.NotExitsCategoryCode, fixedAssetCreateDto.fixed_asset_category_code));
                        }

                        // Cập nhật tỷ lệ hao mòn năm 
                        if (fixedAssetCreateDto.life_time != 0)
                        {
                            var rate = (float)(1f / fixedAssetCreateDto.life_time) * 100;
                            fixedAssetCreateDto.depreciation_rate = (float)Math.Round(rate, 2);
                        }

                        // Cập nhật giá trị hao mòn năm
                        if (fixedAssetCreateDto.cost != 0 && fixedAssetCreateDto.life_time != 0)
                        {
                            var depreciation = (decimal)(fixedAssetCreateDto.cost * (decimal)(fixedAssetCreateDto.depreciation_rate ?? 1) / 100);
                            fixedAssetCreateDto.depreciation_value_year = Math.Round(depreciation, 0);
                        }

                        // Thêm vào list
                        fixedAssetCreateDtos.Add(fixedAssetCreateDto);
                    }
                    catch (Exception ex)
                    {
                        if (ex is Domain.InvalidDataException)
                        {
                            throw;
                        }
                        else
                        {
                            errorRows.Add(row);
                        }
                    }
                }
                if (errorRows.Count > 0)
                {
                    throw new Domain.InvalidDataException(String.Format(ResourcesVI.InvalidRow, string.Join(", ", errorRows)));
                }
            }

            return fixedAssetCreateDtos;
        }
    }

}
