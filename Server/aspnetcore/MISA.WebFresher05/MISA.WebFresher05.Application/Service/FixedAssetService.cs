using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher05.Domain.Resources;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Service của tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IFixedAssetManager _fixedAssetManager;
        private readonly IFixedAssetExcelHandler _fixedAssetExcelHandler;

        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IMapper mapper, IFixedAssetManager fixedAssetManager, IUnitOfWork unitOfWork, IFixedAssetExcelHandler fixedAssetExcelHandler) : base(fixedAssetRepository, mapper, unitOfWork)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetManager = fixedAssetManager;
            _fixedAssetExcelHandler = fixedAssetExcelHandler;
        }

        /// <summary>
        /// Trả về mã tài sản mặc định
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (20/07/2023) 
        public async Task<string?> DefaultCodeAsync()
        {
            var defaultCode = await _fixedAssetRepository.DefaultCodeAsync();

            return defaultCode;
        }

        /// <summary>
        /// Chuyển từ FixedAssetCreateDto sang FixedAsset
        /// </summary>
        /// <param name="fixedAssetCreateDto">FixedAssetCreateDto</param>
        /// <returns>FixedAsset</returns>
        /// Created by: vtahoang (19/07/2023) 
        public async override Task<FixedAsset> MapCreateDtoToEntity(FixedAssetCreateDto fixedAssetCreateDto)
        {
            var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetCreateDto);

            if (fixedAssetCreateDto.fixed_asset_code != null)
            {
                await _fixedAssetManager.CheckFixedAssetExitsByCodeAsync(code: fixedAssetCreateDto.fixed_asset_code);
            }

            // Cập nhật các trường mặc định
            fixedAsset.fixed_asset_id = Guid.NewGuid();
            fixedAsset.tracked_year = DateTime.Now.Year;
            fixedAsset.active = true;
            fixedAsset.production_year = fixedAsset.start_using_date?.Year;

            // Cập nhật giá trị hao mòn và giá trị còn lại
            var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
            if (temp > fixedAsset.cost)
            {
                fixedAsset.accumulated_depreciation = fixedAsset.cost;
            }
            else
            {
                fixedAsset.accumulated_depreciation = temp;
            }
            fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

            // Kiểm tra dữ liệu
            await _fixedAssetManager.ValidateData(fixedAsset);
            return fixedAsset;
        }

        /// <summary>
        /// Chuyển từ FixedAssetUpdateDto sang FixedAsset
        /// </summary>
        /// <param name="entityUpdateDto">FixedAssetUpdateDto</param>
        /// <returns>FixedAsset</returns>
        /// Created by: vtahoang (19/07/2023) 
        public async override Task<FixedAsset> MapUpdateDtoToEntity(FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetUpdateDto);

            // Nếu sinh mã mới thì kiểm tra xem mã code mới đã tồn tại chưa
            if (fixedAssetUpdateDto.fixed_asset_code != null)
            {
                await _fixedAssetManager.CheckFixedAssetCodeExitsByCodeForUpdateAsync(fixedAssetUpdateDto.fixed_asset_code, fixedAssetUpdateDto.fixed_asset_id);
            }

            // Cập nhật các trường mặc định
            fixedAsset.production_year = fixedAsset.start_using_date?.Year;

            // Cập nhật giá trị hao mòn
            var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
            if (temp > fixedAsset.cost)
            {
                fixedAsset.accumulated_depreciation = fixedAsset.cost;
            }
            else
            {
                fixedAsset.accumulated_depreciation = temp;
            }

            // Cập nhật giá trị còn lại
            fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;
            // kiểm tra dữ liệu
            await _fixedAssetManager.ValidateData(fixedAsset);
            return fixedAsset;
        }

        /// <summary>
        /// Xuất dữ liệu ra excel
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        public async Task<byte[]> ExportToExcel(FilterObject filterObject, ExcelOptions excelOptions)
        {
            // Bỏ limit để lấy toàn bộ dữ liệu được lọc
            filterObject.Limit = 0;

            var fixedAssets = await _fixedAssetRepository.GetFilterAsync(filterObject);

            var fixedAssetDtos = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssets);

            var fileBytes = await _fixedAssetExcelHandler.ExportToExcel(fixedAssetDtos, excelOptions);

            return fileBytes;
        }

        /// <summary>
        /// File mẫu nhập liệu 
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        public async Task<byte[]> ExampleFileExcel()
        {
            var columns = new List<ExcelColumn>
            {
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.AssetCode,
                    FieldName = "fixed_asset_code",
                    DataType = DataType.Text,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.AssetName,
                    FieldName = "fixed_asset_name",
                    DataType = DataType.Text,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.DepartmentCode,
                    FieldName = "department_code",
                    DataType = DataType.Text,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.AssetCategoryCode,
                    FieldName = "fixed_asset_category_code",
                    DataType = DataType.Text,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.PurchaseDate,
                    FieldName = "purchase_date",
                    DataType = DataType.Date,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.StartUsingDate,
                    FieldName = "start_using_date",
                    DataType = DataType.Date,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.Cost,
                    FieldName = "cost",
                    DataType = DataType.Number,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.Quantity,
                    FieldName = "quantity",
                    DataType = DataType.Number,
                },
                new ExcelColumn
                {
                    ColumnName = ResourcesVI.LifeTime,
                    FieldName = "life_time",
                    DataType = DataType.Number,
                }
            };


            var excelOptions = new ExcelOptions
            {
                FileName = $"{ResourcesVI.ExcelExampleInput}.xlsx",
                SheetName = ResourcesVI.ExcelExampleInput,
                Columns = columns
            };

            var fixedAsset = await _fixedAssetRepository.FindOneAsync();
            var fixedAssetDto = _mapper.Map<FixedAssetDto>(fixedAsset);

            var fileBytes = await _fixedAssetExcelHandler.ExampleFileExcel(fixedAssetDto, excelOptions);

            return fileBytes;
        }

        /// <summary>
        /// Nhập dữ liệu từ file excel
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        public async Task ImportFromExcel(byte[] fileBytes)
        {
            // Lấy danh sách tài sản từ file excel
            var assetsList = await _fixedAssetExcelHandler.GetDataFromExcel(fileBytes);

            // Thêm mới danh sách tài sản
            await InsertMany(assetsList);

        }

        /// <summary>
        /// Tạo mới nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        public async Task InsertMany(IEnumerable<FixedAssetCreateDto> fixedAssetDtosList)
        {
            // Mở transaction
            await _uow.BeginTransactionAsync();
            try
            {
                // Lấy danh sách mã tài sản để kiểm tra xem đã tồn tại chưa
                var listCode = fixedAssetDtosList.Select(x => (string)(x.fixed_asset_code ?? "")).ToList();

                // Kiểm tra xem danh sách mã có giá trị trùng lặp không
                if (listCode.Count != listCode.Distinct().Count())
                {
                    throw new Domain.InvalidDataException(ResourcesVI.ConflictAssetCodeError);
                }

                // Kiểm tra xem có mã tài sản nào tồn tại chưa
                await _fixedAssetManager.CheckListFixedAssetExitsByCodeAsync(listCode);

                var fixedAssets = new List<FixedAsset>();

                // Chuyển Dto thành Entity
                foreach (var fixedAssetDto in fixedAssetDtosList)
                {
                    var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetDto);

                    // Cập nhật các trường mặc định
                    fixedAsset.fixed_asset_id = Guid.NewGuid();
                    fixedAsset.tracked_year = DateTime.Now.Year;
                    fixedAsset.active = true;
                    fixedAsset.production_year = fixedAsset.start_using_date?.Year;
                    fixedAsset.modified_date = DateTime.Now;
                    fixedAsset.modified_by = "";
                    fixedAsset.created_date = DateTime.Now;
                    fixedAsset.created_by = "";

                    // Cập nhật giá trị hao mòn
                    var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
                    if (temp > fixedAsset.cost)
                    {
                        fixedAsset.accumulated_depreciation = fixedAsset.cost;
                    }
                    else
                    {
                        fixedAsset.accumulated_depreciation = temp;
                    }
                    // Cập nhật giá trị còn lại
                    fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

                    // Kiểm tra dữ liệu
                    await _fixedAssetManager.ValidateData(fixedAsset);

                    fixedAssets.Add(fixedAsset);
                }

                // Thêm mới danh sách tài sản
                await _fixedAssetRepository.CreateManyAsync(fixedAssets);

                // Lấy mã tài sản lớn nhất
                var code = fixedAssetDtosList.Max(asset => asset.fixed_asset_code);
                // Cập nhật mã tự sinh
                if (!string.IsNullOrEmpty(code))
                {
                    await _fixedAssetRepository.UpdateCodeAsync(code);
                }

                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetUpdateDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (25/08/2023) 
        public async Task UpdateManyAsync(IEnumerable<FixedAssetUpdateDto> fixedAssetUpdateDtosList)
        {
            var fixedAssets = _mapper.Map<IEnumerable<FixedAsset>>(fixedAssetUpdateDtosList);

            foreach (var fixedAsset in fixedAssets)
            {
                // Cập nhật các trường mặc định
                fixedAsset.tracked_year = DateTime.Now.Year;
                fixedAsset.active = true;
                fixedAsset.production_year = fixedAsset.start_using_date?.Year;
                fixedAsset.modified_date = DateTime.Now;
                fixedAsset.modified_by = "";

                fixedAsset.depreciation_value_year = fixedAsset.cost * (decimal)(fixedAsset.depreciation_rate ?? 0) / 100;

                // Cập nhật giá trị hao mòn
                var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
                if (temp > fixedAsset.cost)
                {
                    fixedAsset.accumulated_depreciation = fixedAsset.cost;
                }
                else
                {
                    fixedAsset.accumulated_depreciation = temp;
                }
                // Cập nhật giá trị còn lại
                fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

                // Kiểm tra dữ liệu
                await _fixedAssetManager.ValidateData(fixedAsset);
            }


            await _fixedAssetRepository.UpdateManyAsync(fixedAssets);
        }

        /// <summary>
        /// Danh sách tài sản chưa được ghi tăng theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        public async Task<IEnumerable<FixedAssetDto>> GetFixedAssetsUnIncrease(FilterObject filterObject)
        {
            var fixedAssets = await _fixedAssetRepository.GetFilterAsync(filterObject, "asset_increase_voucher_detail");

            var fixedAssetDtos = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssets);

            return fixedAssetDtos;
        }

        /// <summary>
        /// Tổng số tài sản chưa được ghi tăng
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023)
        public async Task<int> GetTotalUnIncrease(FilterObject filterObject)
        {
            var result = await _fixedAssetRepository.GetTotalRecordsFilterAsync(filterObject, "asset_increase_voucher_detail");

            return result;
        }

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (11/09/2023) 
        public override async Task DeleteAsync(Guid id)
        {
            var fixedAsset = await _fixedAssetRepository.GetAsync(id);

            // Kiểm tra xem tài sản đã có chứng từ chưa
            await _fixedAssetManager.CheckFixedAssetsIncreased(new List<FixedAsset> { fixedAsset });

            await _fixedAssetRepository.DeleteAsync(fixedAsset);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (11/09/2023) 
        public override async Task DeleteManyAsync(List<Guid> ids)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                // Danh sach rỗng
                if (ids.Count == 0)
                {
                    throw new Exception(ResourcesVI.NullArrayError);
                }

                var entities = await _fixedAssetRepository.GetListByIdsAsync(ids);

                // Dữ liệu truyển không đúng
                if (entities.ToList().Count < ids.Count)
                {
                    throw new Exception(ResourcesVI.CantDelete);
                }

                // Kiểm tra xem tài sản đã có chứng từ chưa
                await _fixedAssetManager.CheckFixedAssetsIncreased(entities);

                await _fixedAssetRepository.DeleteManyAsync(entities);

                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }
        }
    }
}
