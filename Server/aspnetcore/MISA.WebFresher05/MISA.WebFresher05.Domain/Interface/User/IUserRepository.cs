using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public interface IUserRepository
    {
        /// <summary>
        /// Lấy thông tin user theo tài khoản, email hoặc số điện thoại
        /// </summary>
        /// <param name="account">Tài khoản, email hoặc số điện thoại</param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        Task<User> GetUserAsync(string account);
    }
}
