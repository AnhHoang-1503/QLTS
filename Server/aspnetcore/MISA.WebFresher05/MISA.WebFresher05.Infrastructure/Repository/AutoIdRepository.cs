using Dapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Repository sinh mã 
    /// </summary>
    /// Created by: vtahoang (15/08/2023) 
    public class AutoIdRepository : IAutoIdRepository
    {
        private readonly IUnitOfWork _uow;

        private readonly ICodeHandler _codeHandler;

        public AutoIdRepository(IUnitOfWork uow, ICodeHandler codeHandler)
        {
            _uow = uow;
            _codeHandler = codeHandler;
        }

        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Mã mới</returns>
        /// Created by: vtahoang (15/08/2023)
        public async Task<string> GetNewCode(string tableName)
        {
            var sql = $"SELECT * FROM auto_id WHERE table_name = @table_name";

            var param = new DynamicParameters();

            param.Add("table_name", tableName);

            var autoId = await _uow.Connection.QueryFirstOrDefaultAsync<AutoId>(sql, param, transaction: _uow.Transaction);

            // Giá trị mặc định
            var code = "00001";

            if (autoId != null)
            {
                code = await _codeHandler.GetCode(autoId);
            }

            return code;
        }

        /// <summary>
        /// Cập nhật mã mới
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="code">Mã mới</param>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        public async Task UpdateCode(string tableName, string code)
        {
            var sql = "Proc_auto_id_update";

            var param = new DynamicParameters();

            var autoId = await _codeHandler.Split(code);

            param.Add("$table_name", tableName);
            param.Add("$prefix", autoId.prefix);
            param.Add("$suffix", autoId.suffix);
            param.Add("$base_value", autoId.base_value + 1);
            param.Add("$value_length", autoId.value_length);

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
