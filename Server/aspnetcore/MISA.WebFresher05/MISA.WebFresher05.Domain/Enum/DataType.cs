using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Kiểu dữ liệu
    /// </summary>
    /// Created by: vtahoang (05/08/2023) 
    public enum DataType
    {
        /// <summary>
        /// Kiểu dữ liệu chuỗi
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        Text,
        /// <summary>
        /// Kiểu dữ liệu số
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        Number,
        /// <summary>
        /// Kiểu dữ liệu ngày tháng
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        Date,
        /// <summary>
        /// Kiểu dữ liệu phần trăm
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        Percentage,
    }
}
