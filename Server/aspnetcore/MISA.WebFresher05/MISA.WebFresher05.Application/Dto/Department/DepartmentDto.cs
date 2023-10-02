using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Dto của phòng ban
    /// </summary>
    /// Created by: vtahoang (13/07/2023) 
    public class DepartmentDto
    {
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        [Required]
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã code phòng ban 
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        [Required(ErrorMessage = "Mã phòng ban không được phép để trống")]
        [MaxLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: vtahoang (13/07/2023)
        [Required(ErrorMessage = "Tên phòng ban không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban không được vượt quá 255 ký tự")]
        public string? department_name { get; set; }

    }
}
