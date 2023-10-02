using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Các thuộc tính của file excel
    /// </summary>
    /// Created by: vtahoang (05/08/2023) 
    public class ExcelOptions
    {
        /// <summary>
        /// Tên sheet
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        public string? SheetName { get; set; } = "Sheet1";

        /// <summary>
        /// Tên file
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        public string? FileName { get; set; }

        /// <summary>
        /// Tiêu đề cột
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        public List<ExcelColumn>? Columns { get; set; }

    }
}
