using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Dto cập nhật tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public class FixedAssetUpdateDto
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
        [Required(ErrorMessage = "Mã tài sản không được phép để trống.")]
        [MaxLength(100, ErrorMessage = "Mã tài sản không được vượt quá 100 ký tự.")]
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Tên tài sản không được phép để trống.")]
        [MaxLength(255, ErrorMessage = "Tên tài sản không được vượt quá 255 ký tự.")]
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required]
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Mã phòng ban không được phép để trống.")]
        [MaxLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự.")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Tên phòng ban không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban không được vượt quá 255 ký tự")]
        public string? department_name { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required]
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Mã loại tài sản không được phép để trống")]
        [MaxLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự")]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên tài sản 
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Tên loại tài sản không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên loại tài sản không được vượt quá 255 ký tự")]
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Ngày mua không được phép để trống")]
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: vtahoang (20/07/2023) 
        [Required(ErrorMessage = "Ngày bắt đầu sử dụng không được phép để trống")]
        public DateTime? start_using_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Nguyên giá không được phép để trống")]
        public decimal? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Số lượng không được phép để trống")]
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Tỷ lệ hao mòn không được phép để trống")]
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        /// Created by: vtahoang (20/07/2023) 
        [Required(ErrorMessage = "Giá trị hao mòn năm không được phép để trống")]
        public decimal? depreciation_value_year { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Năm bắt đầu theo dõi tài sản trên phần mềm không được phép để trống")]
        public int? tracked_year { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Số năm sử dụng không được phép để trống")]
        public int? life_time { get; set; }

        /// <summary>
        /// Nguồn hình thành
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public string? budget { get; set; }
    }
}
