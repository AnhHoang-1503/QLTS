using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure.Authentication
{
    public class JwtOptions
    {
        /// <summary>
        /// Mã khoá
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string SecretKey { get; set; }

        /// <summary>
        /// Người tạo token
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string Issuer { get; set; }

        /// <summary>
        /// Người dùng
        /// </summary>
        /// Created by: vtahoang (16/09/2023) 
        public string Audience { get; set; }
    }
}
