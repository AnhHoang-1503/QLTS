using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public interface IJwtHandler
    {
        /// <summary>
        /// Tạo token jwt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public string Generate(User user);

        /// <summary>
        /// Kiểm tra token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public Task Verify(string token);
    }
}
