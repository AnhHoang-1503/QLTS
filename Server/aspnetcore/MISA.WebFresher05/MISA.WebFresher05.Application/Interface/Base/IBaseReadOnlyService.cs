using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Interface base service chỉ đọc
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Created by: vtahoang (14/07/2023)
    public interface IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (14/07/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: vtahoang (14/07/2023)
        Task<TEntityDto> GetAsync(Guid id);

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: vtahoang (14/07/2023)
        Task<TEntityDto?> FindAsync(Guid id);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        Task<int> GetTotalRecordsAsync();

        /// <summary>
        /// Lấy bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Created by: vtahoang (22/08/2023) 
        Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Tổng số bản ghi thoả mãn bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject);

        /// <summary>
        /// Danh sách bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject"></param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        Task<IEnumerable<TEntityDto>> GetFilterAsync(FilterObject filterObject);
    }
}
