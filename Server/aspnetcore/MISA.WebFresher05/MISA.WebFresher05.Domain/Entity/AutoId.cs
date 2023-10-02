using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Entity sinh mã tự động
    /// </summary>
    /// Created by: vtahoang (15/08/2023) 
    public class AutoId
    {
        /// <summary>
        /// Tiền tố
        /// </summary>
        /// Created by: vtahoang (15/08/2023) 
        public string? prefix { get; set; }

        /// <summary>
        /// Hậu tố
        /// </summary>
        /// Created by: vtahoang (15/08/2023) 
        public string? suffix { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        /// Created by: vtahoang (15/08/2023) 
        public int? base_value { get; set; }

        /// <summary>
        /// Độ dài phần giá trị
        /// </summary>
        /// Created by: vtahoang (15/08/2023) 
        public int value_length { get; set; }
    }
}
