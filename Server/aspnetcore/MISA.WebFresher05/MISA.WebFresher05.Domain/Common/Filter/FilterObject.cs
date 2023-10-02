using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Bộ lọc dữ liệu
    /// </summary>
    /// Created by: vtahoang (01/08/2023) 
    public class FilterObject
    {
        // Số bản ghi một trang
        public int Limit { get; set; }
        // Số bản ghi bỏ qua
        public int Offset { get; set; }
        // Sắp xếp theo trường
        public string? SortField { get; set; }
        // Kiểu sắp xếp ASC - DESC
        public string? SortType { get; set; }
        // Cột lấy dữ liệu
        public string? Columns { get; set; }
        // Điều kiện lọc
        public List<FilterItem>? Filter { get; set; }
        // Tìm kiếm 
        public List<FilterItem>? Search { get; set; }

    }

}
