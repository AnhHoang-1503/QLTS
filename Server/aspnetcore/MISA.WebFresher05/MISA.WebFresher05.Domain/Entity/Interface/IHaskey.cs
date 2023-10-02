
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface của các bảng có key
    /// </summary>
    /// Created by: vtahoang (18/07/2023) 
    public interface IHaskey
    {
        /// <summary>
        /// Trả về key của bàng
        /// </summary>
        /// <returns>Key của bảng</returns>
        /// Created by: vtahoang (18/07/2023) 
        Guid GetKey();
    }
}
