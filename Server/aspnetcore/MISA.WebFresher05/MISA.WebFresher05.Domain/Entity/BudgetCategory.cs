using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Nguồn hình thành
    /// </summary>
    /// Created by: vtahoang (24/08/2023) 
    public class BudgetCategory
    {
        /// <summary>
        /// Id nguồn hình thành
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public Guid budget_category_id { get; set; }

        /// <summary>
        /// Mã nguồn hình thành
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? budget_category_code { get; set; }

        /// <summary>
        /// Tên nguồn hình thành
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? budget_category_name { get; set; }
    }
}
