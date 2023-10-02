using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface base cho repository chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: vtahoang (18/07/2023) 
    public interface IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả entity
        /// </summary>
        /// <returns>Danh sách entity</returns>
        /// Created by: vtahoang (18/07/2023) 
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="id">Id entity</param>
        /// <returns>Entity theo id</returns>
        /// Created by: vtahoang (18/07/2023) 
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Tìm entity theo id
        /// </summary>
        /// <param name="id">Id entity</param>
        /// <returns>Entity theo id hoặc null</returns>
        /// Created by: vtahoang (18/07/2023) 
        Task<TEntity?> FindAsync(Guid id);

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Created by: vtahoang (22/07/2023) 
        Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        Task<int> GetTotalRecordsAsync();

        /// <summary>
        /// Số bản ghi có mã hoặc id tương ứng
        /// </summary>
        /// <param name="code">Mã định danh</param>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số bản ghi</returns>
        /// Created by: vtahoang (08/08/2023) 
        public Task<int> CountByCodeOrId(string code, Guid? id);

        /// <summary>
        /// Lấy danh sách bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        Task<IEnumerable<TEntity>> GetFilterAsync(FilterObject filterObject);

        /// <summary>
        /// Tổng số bản ghi thoả mãn bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject);
    }
}
