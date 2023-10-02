using Dapper;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Base repository chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: vtahoang (19/07/2023)
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        protected readonly IUnitOfWork _uow;

        private readonly IFilterObjectHandler _filterObjectHandler;

        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name.ToLower();

        /// <summary>
        /// Tên cột id
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public virtual string TableId { get; protected set; } = typeof(TEntity).Name.ToLower() + "_id";

        /// <summary>
        /// Tên cột code    
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public virtual string TableCode { get; protected set; } = typeof(TEntity).Name.ToLower() + "_code";

        public BaseReadOnlyRepository(IUnitOfWork unitOfWork, IFilterObjectHandler filterObjectHandler)
        {
            _uow = unitOfWork;
            _filterObjectHandler = filterObjectHandler;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (18/07/2023) 
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            //var sql = $"SELECT * FROM {TableName}";
            var sql = $"Proc_{TableName}_GetAll";

            var result = await _uow.Connection.QueryAsync<TEntity>(sql, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi theo id</returns>
        /// <exception cref="NotFoundException">Không tìm thấy thì trả về exception</exception>
        /// Created by: vtahoang (18/07/2023) 
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(ResourcesVI.NotFound);
            }

            return entity;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi theo id</returns>
        /// Created by: vtahoang (18/07/2023) 
        public async Task<TEntity?> FindAsync(Guid id)
        {
            var sql = $"Proc_{TableName}_Get";

            var param = new DynamicParameters();

            param.Add("$id", id);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo list ids
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        public async Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableId} IN @ListIds;";

            var param = new DynamicParameters();

            param.Add("ListIds", ids);

            var entities = await _uow.Connection.QueryAsync<TEntity>(sql, param, transaction: _uow.Transaction);

            return entities;
        }

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetTotalRecordsAsync()
        {
            var sql = $"SELECT COUNT(*) FROM {TableName}";

            var totalRecords = await _uow.Connection.QueryFirstOrDefaultAsync<int>(sql, transaction: _uow.Transaction);

            return totalRecords;
        }

        /// <summary>
        /// Số bản ghi có mã hoặc id tương ứng
        /// </summary>
        /// <param name="code">Mã định danh</param>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số bản ghi</returns>
        /// Created by: vtahoang (08/08/2023) 
        public async Task<int> CountByCodeOrId(string code, Guid? id)
        {
            var sql = $"SELECT COUNT(*) FROM {TableName} WHERE {TableCode} = @code OR {TableId} = @id;";

            var param = new DynamicParameters();

            param.Add("code", code);
            param.Add("id", id);

            var count = await _uow.Connection.QueryFirstOrDefaultAsync<int>(sql, param, transaction: _uow.Transaction);

            return count;
        }


        /// <summary>
        /// Lấy danh sách bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (31/07/2023) 
        public async Task<IEnumerable<TEntity>> GetFilterAsync(FilterObject filterObject)
        {
            var sql = "Proc_GetFilter";

            var param = new DynamicParameters();

            param.Add("$columns", filterObject.Columns);
            param.Add("$sort_field", filterObject.SortField);
            param.Add("$sort_type", filterObject.SortType);
            param.Add("$offset", filterObject.Offset);
            param.Add("$limit", filterObject.Limit);
            param.Add("$table", TableName);

            var whereCondition = await _filterObjectHandler.CreateWhereCondition(filterObject);

            param.Add("$where_condition", whereCondition);

            var result = await _uow.Connection.QueryAsync<TEntity>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }


        /// <summary>
        /// Lấy tổng số bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns>Tổng số bản ghi</returns>
        /// Created by: vtahoang (31/07/2023) 
        public async Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject)
        {
            var sql = "Proc_GetTotalRecords";

            var param = new DynamicParameters();

            var whereCondition = await _filterObjectHandler.CreateWhereCondition(filterObject);

            param.Add("$where_condition", whereCondition);
            param.Add("$table", TableName);

            var totalRecords = await _uow.Connection.QueryFirstOrDefaultAsync<int>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

            return totalRecords;
        }

    }
}
