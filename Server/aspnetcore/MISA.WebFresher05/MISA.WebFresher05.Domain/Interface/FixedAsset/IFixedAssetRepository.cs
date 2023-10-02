using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface repository của tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        /// <summary>
        /// Tìm tài sản theo code
        /// </summary>
        /// <param name="code">Code tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        Task<FixedAsset?> FindByCodeAsync(string code);

        /// <summary>
        /// Tìm một bản ghi trong bảng
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (05/08/2023) 
        Task<FixedAsset?> FindOneAsync();

        /// <summary>
        /// Tìm kiếm bản ghi theo danh sách code
        /// </summary>
        /// <param name="codes">Danh sách code</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        Task<IEnumerable<FixedAsset>> FindByListCodeAsync(List<string> codes);

        /// <summary>
        /// Lấy danh sách tài sản không nằm trong bảng khác
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <param name="otherTable">Tên bảng khác</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        Task<IEnumerable<FixedAsset>> GetFilterAsync(FilterObject filterObject, string otherTable);

        /// <summary>
        /// Tổng số bản ghi thoả mãn bộ lọc và không nằm trong bảng khác
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <param name="otherTable">Tên bảng khác</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject, string otherTable);

        /// <summary>
        /// Xoá nguồn hình thành nhiều tài sản 
        /// </summary>
        /// <param name="ids">Danh sách tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (29/08/2023) 
        Task DeleteBudgetManyAssets(List<Guid> ids);
    }
}
