using Dapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static OfficeOpenXml.ExcelErrorValue;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Base repository thêm sửa xoá
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: vtahoang (19/07/2023)
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHaskey
    {
        protected readonly IAutoIdRepository _autoIdRepository;

        private readonly IFilterObjectHandler _filterObjectHandler;

        protected BaseRepository(IUnitOfWork unitOfWork, IAutoIdRepository autoIdRepository, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, filterObjectHandler)
        {
            _autoIdRepository = autoIdRepository;
            _filterObjectHandler = filterObjectHandler;
        }

        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">Bản ghi thêm</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task CreateAsync(TEntity entity)
        {
            var sql = $"Proc_{TableName}_Insert";

            var param = new DynamicParameters();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "$" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);

            }

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Sửa bản ghi 
        /// </summary>
        /// <param name="entity">Bản ghi sửa</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task UpdateAsync(TEntity entity)
        {
            var sql = $"Proc_{TableName}_Update";

            var param = new DynamicParameters();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "$" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task DeleteAsync(TEntity entity)
        {
            var sql = $"DElETE From {TableName} WHERE {TableId} = @id;";

            var param = new DynamicParameters();

            param.Add("id", entity.GetKey());

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi cần xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            var sql = $"DElETE From {TableName} WHERE {TableId} IN @ListIds;";

            var param = new DynamicParameters();

            param.Add("ListIds", entities.Select(x => x.GetKey()));

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
        }


        /// <summary>
        /// Trả về mã mặc định
        /// </summary>
        /// <returns>Mã mặc định</returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<string?> DefaultCodeAsync()
        {
            var code = await _autoIdRepository.GetNewCode(TableName);

            return code;
        }

        /// <summary>
        /// Cập nhật mã sinh tự động
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        public async Task UpdateCodeAsync(string code)
        {
            await _autoIdRepository.UpdateCode(TableName, code);
        }

        /// <summary>
        /// Tạo nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns></returns>
        public async Task CreateManyAsync(IEnumerable<TEntity> entities)
        {

            var properties = typeof(TEntity).GetProperties();

            var columns = "(";
            var values = "(";
            // Thêm các tên cột vào câu lệnh
            foreach (var property in properties)
            {
                columns += $"{property.Name}, ";
                values += $"@{property.Name}, ";
            }

            // Xóa dấu phẩy cuối cùng
            columns = columns.Remove(columns.Length - 2, 2);
            columns += ")";
            values = values.Remove(values.Length - 2, 2);
            values += ")";

            var sql = $"INSERT INTO {TableName} {columns} VALUES {values} ";

            // Tạo danh sách các tham số
            var listParam = new List<DynamicParameters>();

            foreach (var entity in entities)
            {
                var param = new DynamicParameters();

                foreach (var property in properties)
                {
                    param.Add(property.Name, property.GetValue(entity));
                }

                listParam.Add(param);
            }

            await _uow.Connection.ExecuteAsync(sql, listParam, transaction: _uow.Transaction);
        }

        /// <summary>
        /// Sửa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (23/08/2023) 
        public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            var properties = typeof(TEntity).GetProperties();
            var set = "";

            // Thêm các tên cột vào câu lệnh
            foreach (var property in properties)
            {
                if (property.Name == "created_date" || property.Name == "created_by") continue;
                set += $"{property.Name} = @{property.Name}, ";
            }
            set = set.Remove(set.Length - 2, 2);

            var sql = $"UPDATE {TableName} SET {set} WHERE {TableId} = @{TableId} ";

            // Tạo danh sách các tham số
            var listParam = new List<DynamicParameters>();

            foreach (var entity in entities)
            {
                var param = new DynamicParameters();

                foreach (var property in properties)
                {
                    param.Add(property.Name, property.GetValue(entity));
                }

                listParam.Add(param);
            }

            var result = await _uow.Connection.ExecuteAsync(sql, listParam, transaction: _uow.Transaction);

        }
    }
}
