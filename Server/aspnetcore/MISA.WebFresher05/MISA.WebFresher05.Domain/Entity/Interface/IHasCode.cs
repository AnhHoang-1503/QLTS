using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface của các bảng có mã
    /// </summary>
    public interface IHasCode
    {
        /// <summary>
        /// Trả về mã
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        string GetCode();
    }
}
