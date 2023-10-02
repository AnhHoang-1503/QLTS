using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public interface IIncreaseVoucherManager
    {
        /// <summary>
        /// Kiểm tra mã chứng từ đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        Task CheckCodeExitsAsync(string code);

        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại chưa cho update
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="id">Id tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        Task CheckCodeExitsForUpdateAsync(string code, Guid id);
    }
}
