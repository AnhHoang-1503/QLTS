using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Dto của loại tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public class FixedAssetCategoryDto
    {
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required]
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã code loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Mã loại tài sản không được phép để trống")]
        [MaxLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự")]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        [Required(ErrorMessage = "Tên loại tài sản không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên loại tài sản không được vượt quá 255 ký tự")]
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: vtahoang (21/07/2023) 
        [Required(ErrorMessage = "Số năm sử dụng không được phép để trống")]
        public int? life_time { get; set; }
    }
}
