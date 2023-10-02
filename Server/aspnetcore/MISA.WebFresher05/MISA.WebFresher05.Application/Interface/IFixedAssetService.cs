using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Interface service của tài sản
    /// </summary>
    /// Created by: vtahoang (20/07/2023) 
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        /// <summary>
        /// Trả về mã tài sản mặc định
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (20/07/2023) 
        Task<string?> DefaultCodeAsync();

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        Task InsertMany(IEnumerable<FixedAssetCreateDto> fixedAssetDtosList);

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        Task<byte[]> ExportToExcel(FilterObject filterObject, ExcelOptions excelOptions);

        /// <summary>
        /// File mẫu nhập liệu 
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        Task<byte[]> ExampleFileExcel();

        /// <summary>
        /// Nhập dữ liệu từ file excel
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        Task ImportFromExcel(byte[] fileBytes);

        /// <summary>
        /// Cập nhật dữ liệu nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetUpdateDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (25/08/2023) 
        Task UpdateManyAsync(IEnumerable<FixedAssetUpdateDto> fixedAssetUpdateDtosList);

        /// <summary>
        /// Danh sách tài sản chưa được ghi tăng theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        Task<IEnumerable<FixedAssetDto>> GetFixedAssetsUnIncrease(FilterObject filterObject);

        /// <summary>
        /// Tổng số tài sản chưa được ghi tăng
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        Task<int> GetTotalUnIncrease(FilterObject filterObject);
    }
}
