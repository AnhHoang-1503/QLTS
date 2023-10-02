using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public class IncreaseVoucher : BaseAuditEntity, IHaskey, IHasCode
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
        /// Lấy mã chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        /// Created by: vtahoang (22/08/2023) 
        public string GetCode()
        {
            return ref_no ?? "";
        }

        /// <summary>
        /// Lấy khóa chứng từ
        /// </summary>
        /// <returns>Khoá chứng từ</returns>
        /// Created by: vtahoang (22/08/2023) 
        public Guid GetKey()
        {
            return ref_id;
        }
    }
}
