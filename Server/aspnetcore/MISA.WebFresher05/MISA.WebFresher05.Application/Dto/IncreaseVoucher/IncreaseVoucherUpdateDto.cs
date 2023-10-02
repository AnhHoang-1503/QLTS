using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    public class IncreaseVoucherUpdateDto
    {
        /// <summary>
        /// Định danh chứng từ
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public Guid ref_id { get; set; }

        /// <summary>
        /// Mã chứng từ 
        /// </summary>
        /// Created by: vtahoang (22/08/2023)
        [Required(ErrorMessage = "Mã chứng từ không được để trống")]
        [MaxLength(50, ErrorMessage = "Mã chứng từ không được vượt quá 50 ký tự")]
        public string? ref_no { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public DateTime ref_date { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public DateTime increase_date { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created by: vtahoang (22/08/2023) 
        public string? note { get; set; }

        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public decimal? total_cost { get; set; }

        /// <summary>
        /// Danh sách chi tiết ghi tăng
        /// </summary>
        /// Created by: vtahoang (23/08/2023) 
        public List<DetailObjectIncreaseVoucherDto>? Details { get; set; }
    }
}
