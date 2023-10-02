using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Chi tiết ghi tăng
    /// </summary>
    /// Created by: vtahoang (23/08/2023) 
    public class DetailObjectIncreaseVoucherDto
    {
        /// <summary>
        /// Tài sản ghi tăng
        /// </summary>
        /// Created by: vtahoang (23/08/2023) 
        [Required(ErrorMessage = "Tài sản không được để trống")]
        public FixedAssetUpdateDto? FixedAsset { get; set; }

        /// <summary>
        /// Phương thức edit : thêm / sửa / xoá
        /// </summary>
        /// Created by: vtahoang (23/08/2023) 
        public EditMode EditMode { get; set; }
    }
}
