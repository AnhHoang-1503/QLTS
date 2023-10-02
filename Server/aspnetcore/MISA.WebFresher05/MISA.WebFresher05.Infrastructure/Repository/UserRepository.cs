using Dapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;

        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Lấy thông tin user theo tài khoản, email hoặc số điện thoại
        /// </summary>
        /// <param name="account">Tài khoản, email hoặc số điện thoại</param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        async Task<User> IUserRepository.GetUserAsync(string account)
        {
            var sql = $"SELECT * FROM user WHERE user_id = '{account}' OR user_email = '{account}' OR user_phone = '{account}'";

            var user = await _uow.Connection.QueryFirstOrDefaultAsync<User>(sql, transaction: _uow.Transaction);

            return user;
        }
    }
}
