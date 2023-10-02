using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Interface base service có các phương thức cơ bản thêm sửa xóa
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created by: vtahoang (14/07/2023) 
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (14/07/2023)
        Task CreateAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi sửa</param>
        /// <returns></returns>
        /// Created by: vtahoang (14/07/2023)
        Task UpdateAsync(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (14/07/2023)
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (18/07/2023) 
        Task DeleteManyAsync(List<Guid> ids);
    }
}
