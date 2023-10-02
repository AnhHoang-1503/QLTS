using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface base repository thêm, sửa, xoá
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: vtahoang (18/07/2023) 
    public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Tạo entity mới
        /// </summary>
        /// <param name="entity">Entity tạo</param>
        /// <returns></returns>
        /// Created by: vtahoang (18/07/2023) 
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Tạo nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (22/08/2023) 
        Task CreateManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="entity">Entity sửa</param>
        /// <returns></returns>
        /// Created by: vtahoang (18/07/2023) 
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Sửa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (23/08/2023) 
        Task UpdateManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Xoá entity
        /// </summary>
        /// <param name="entity">Entity xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (18/07/2023) 
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (18/07/2023) 
        Task DeleteManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Trả về mã mặc định
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        Task<string?> DefaultCodeAsync();

        /// <summary>
        /// Cập nhật mã sinh tự động
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task UpdateCodeAsync(string code);
    }
}
