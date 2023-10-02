using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface xử lý FilterObject
    /// </summary>
    /// Created by: vtahoang (05/08/2023) 
    public interface IFilterObjectHandler
    {
        /// <summary>
        /// Tạo điều kiện WHERE cho câu lệnh SQL dựa vào FilterObject
        /// </summary>
        /// <param name="filterObject"></param>
        /// <returns></returns>
        /// Created by: vtahoang (05/08/2023) 
        Task<string> CreateWhereCondition(FilterObject filterObject);
    }
}
