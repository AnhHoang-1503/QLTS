using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Các cột trong file excel
    /// </summary>
    /// Created by: vtahoang (05/08/2023) 
    public class ExcelColumn
    {
        /// <summary>
        /// Tiêu đề cột
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        [Required]
        public string? ColumnName { get; set; }

        /// <summary>
        /// Tên trường chứa dữ liệu
        /// </summary>
        /// Created by: vtahoang (05/08/2023)
        [Required]
        public string? FieldName { get; set; }

        /// <summary>
        /// Độ rộng cột
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        public double? ColumnWidth { get; set; }

        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        /// Created by: vtahoang (05/08/2023) 
        public DataType DataType { get; set; } = DataType.Text;

    }
}
