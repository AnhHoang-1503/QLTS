using Dapper;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Repository của tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        /// <summary>
        /// Tên bảng trong database
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        public override string TableName { get; protected set; } = "fixed_asset";

        /// <summary>
        /// Tên cột id trong bảng
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        public override string TableId { get; protected set; } = "fixed_asset_id";

        /// <summary>
        /// Tên cột mã trong bảng
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public override string TableCode { get; protected set; } = "fixed_asset_code";

        private readonly IFilterObjectHandler _filterObjectHandler;

        public FixedAssetRepository(IUnitOfWork unitOfWork, IFilterObjectHandler filterObjectHandler, IAutoIdRepository autoIdRepository) : base(unitOfWork, autoIdRepository, filterObjectHandler)
        {
            _filterObjectHandler = filterObjectHandler;
        }

        /// <summary>
        /// Tìm kiếm tài sản theo mã tài sản 
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<FixedAsset?> FindByCodeAsync(string code)
        {
            var sql = "SELECT * FROM fixed_asset WHERE fixed_asset_code = @code;";

            var param = new DynamicParameters();

            param.Add("code", code);

            var fixedAsset = await _uow.Connection.QueryFirstOrDefaultAsync<FixedAsset>(sql, param);

            return fixedAsset;
        }

        /// <summary>
        /// Tìm một bản ghi
        /// </summary>
        /// <returns>Bản ghi</returns>
        /// Created by: vtahoang (05/08/2023) 
        public async Task<FixedAsset?> FindOneAsync()
        {
            var sql = "SELECT * FROM fixed_asset LIMIT 1;";

            var fixedAsset = await _uow.Connection.QueryFirstOrDefaultAsync<FixedAsset>(sql, transaction: _uow.Transaction);

            return fixedAsset;
        }

        /// <summary>
        /// Tìm bản ghi theo danh sách mã code
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FixedAsset>> FindByListCodeAsync(List<string> codes)
        {
            var sql = "SELECT * FROM fixed_asset WHERE fixed_asset_code IN @codes;";

            var param = new DynamicParameters();

            param.Add("codes", codes);

            var fixedAssets = await _uow.Connection.QueryAsync<FixedAsset>(sql, param, transaction: _uow.Transaction);

            return fixedAssets;
        }

        /// <summary>
        /// Lấy danh sách tài sản không nằm trong bảng khác
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <param name="otherTable">Tên bảng khác</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023)
        public async Task<IEnumerable<FixedAsset>> GetFilterAsync(FilterObject filterObject, string otherTable)
        {
            var sql = "Proc_GetFilter";

            var param = new DynamicParameters();

            var columns = filterObject.Columns;

            columns = columns?.Replace(", ", ", fa.");
            columns = columns?.Replace("fixed_asset_id", "fa.fixed_asset_id");

            param.Add("$columns", columns);
            param.Add("$sort_field", filterObject.SortField);
            param.Add("$sort_type", filterObject.SortType);
            param.Add("$offset", filterObject.Offset);
            param.Add("$limit", filterObject.Limit);
            param.Add("$table", $"fixed_asset AS fa LEFT JOIN {otherTable} AS st ON fa.fixed_asset_id = st.fixed_asset_id");

            var whereCondition = await _filterObjectHandler.CreateWhereCondition(filterObject);

            whereCondition = whereCondition?.Replace("fixed_asset", "fa.fixed_asset");

            // Thêm điều kiện chưa ghi tăng
            if (string.IsNullOrEmpty(whereCondition))
            {
                whereCondition = "WHERE st.fixed_asset_id IS NULL";
            }
            else
            {
                whereCondition += " AND st.fixed_asset_id IS NULL";
            }
            param.Add("$where_condition", whereCondition);

            var result = await _uow.Connection.QueryAsync<FixedAsset>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Tổng số bản ghi thoả mãn bộ lọc và không nằm trong bảng khác
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <param name="otherTable">Tên bảng khác</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        public async Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject, string otherTable)
        {
            var sql = "Proc_GetTotalRecords";

            var param = new DynamicParameters();

            var whereCondition = await _filterObjectHandler.CreateWhereCondition(filterObject);

            whereCondition = whereCondition?.Replace("fixed_asset", "fa.fixed_asset");

            if (string.IsNullOrEmpty(whereCondition))
            {
                whereCondition = "WHERE st.fixed_asset_id IS NULL";
            }
            else
            {
                whereCondition += " AND st.fixed_asset_id IS NULL";
            }

            param.Add("$where_condition", whereCondition);
            param.Add("$table", $"fixed_asset AS fa LEFT JOIN {otherTable} AS st ON fa.fixed_asset_id = st.fixed_asset_id");

            var totalRecords = await _uow.Connection.QueryFirstOrDefaultAsync<int>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return totalRecords;
        }

        /// <summary>
        /// Xoá nguồn hình thành nhiều tài sản 
        /// </summary>
        /// <param name="ids">Danh sách tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (29/08/2023) 
        public async Task DeleteBudgetManyAssets(List<Guid> ids)
        {
            var sql = "Update fixed_asset SET budget = NULL WHERE fixed_asset_id IN @ids;";

            var param = new DynamicParameters();

            param.Add("ids", ids);

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
        }
    }
}
