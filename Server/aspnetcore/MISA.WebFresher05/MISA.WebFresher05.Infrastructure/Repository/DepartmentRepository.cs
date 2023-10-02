using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.Common;
using Dapper;
using System.Data;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Repository của phòng ban
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        /// <summary>
        /// Tên bảng trong database
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        public override string TableName { get; protected set; } = "department";

        public DepartmentRepository(IUnitOfWork unitOfWork, IAutoIdRepository autoIdRepository, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, autoIdRepository, filterObjectHandler)
        {
        }

        /// <summary>
        /// Tìm phòng ban theo mã
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<Department?> FindByCodeAsync(string code)
        {
            var sql = "SELECT * FROM department WHERE department_code = @code";

            var param = new DynamicParameters();

            param.Add("code", code);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<Department>(sql, param, transaction: _uow.Transaction);

            return result;
        }
    }
}
