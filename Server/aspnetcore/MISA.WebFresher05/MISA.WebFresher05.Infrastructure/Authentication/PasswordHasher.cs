using Microsoft.AspNetCore.Identity;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Kiểm tra mật khẩu
        /// </summary>
        /// <param name="password">Mật khẩu truyền vào</param>
        /// <param name="hashPassword">Mật khẩu đã hash trong db</param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023)
        public bool Compare(string password, string hashPassword)
        {
            var verified = BCrypt.Net.BCrypt.Verify(password, hashPassword);

            return verified;
        }
    }
}
