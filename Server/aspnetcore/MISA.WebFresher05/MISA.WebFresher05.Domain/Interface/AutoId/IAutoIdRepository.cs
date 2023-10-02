using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface repository sinh mã
    /// </summary>
    /// Created by: vtahoang (15/08/2023) 
    public interface IAutoIdRepository
    {
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        Task<string> GetNewCode(string tableName);

        /// <summary>
        /// Cập nhật mã mới
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="code">Mã mới</param>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        Task UpdateCode(string tableName, string code);
    }
}
