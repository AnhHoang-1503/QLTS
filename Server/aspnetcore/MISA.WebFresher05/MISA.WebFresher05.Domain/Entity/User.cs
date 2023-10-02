using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public class User : BaseAuditEntity, IHaskey
    {
        /// <summary>
        /// Id định danh người dùng
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public Guid? ref_id { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string? user_id { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string? user_name { get; set; }

        /// <summary>
        /// Email người dùng
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string? user_email { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string? password { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string? user_phone { get; set; }

        public Guid GetKey()
        {
            return ref_id ?? Guid.Empty;
        }
    }
}
