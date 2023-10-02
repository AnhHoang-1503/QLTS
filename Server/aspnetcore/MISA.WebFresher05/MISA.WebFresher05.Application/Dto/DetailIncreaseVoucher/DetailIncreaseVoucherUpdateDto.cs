using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    public class DetailIncreaseVoucherUpdateDto
    {
        /// <summary>
        /// Định danh chi tiết chứng từ
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public Guid ref_detail_id { get; set; }

        /// <summary>
        /// Định danh chứng từ
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public Guid ref_id { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public string? ref_no { get; set; }

        /// <summary>
        /// Định danh tài sản
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created by: vtahoang (23/08/2023) 
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Bộ phận sử dụng
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? department_name { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public decimal? cost { get; set; }

        /// <summary>
        /// Hao mòn năm
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public decimal? depreciation_value_year { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public decimal? remaining_value { get; set; }
    }
}
