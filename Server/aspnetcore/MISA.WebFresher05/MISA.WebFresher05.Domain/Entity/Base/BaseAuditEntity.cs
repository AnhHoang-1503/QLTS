using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Base class cho các entity có audit  
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public abstract class BaseAuditEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public DateTime? created_date { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? created_by { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public DateTime? modified_date { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? modified_by { get; set; }
    }
}
