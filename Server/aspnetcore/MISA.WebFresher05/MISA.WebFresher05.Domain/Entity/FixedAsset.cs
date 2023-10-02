using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Entity của tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public class FixedAsset : BaseAuditEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? department_name { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên tài sản 
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: vtahoang (20/07/2023) 
        public DateTime? start_using_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public decimal? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        /// Created by: vtahoang (20/07/2023) 
        public decimal? depreciation_value_year { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public int? life_time { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public int? production_year { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public bool? active { get; set; }

        /// <summary>
        /// Hao mòn luỹ kế
        /// </summary>
        /// Created by: vtahoang (31/07/2023) 
        public decimal? accumulated_depreciation { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        /// Created by: vtahoang (31/07/2023) 
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Nguồn hình thành
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? budget { get; set; }

        /// <summary>
        /// Trả về mã 
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            return fixed_asset_code ?? "";
        }

        /// <summary>
        /// Trả về khoá
        /// </summary>
        /// <returns>Khoá</returns>
        /// Created by: vtahoang (19/07/2023) 
        public Guid GetKey()
        {
            return fixed_asset_id;
        }
    }
}


