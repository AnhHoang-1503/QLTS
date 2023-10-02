using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface quản lý dữ liệu của phòng ban
    /// </summary>
    /// Created by: vtahoang (19/07/2023)  
    public interface IDepartmenManager
    {
        /// <summary>
        /// Kiểm tra xem mã phòng ban đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        Task CheckDepartmentExitsByCodeAsync(string code);
    }
}
