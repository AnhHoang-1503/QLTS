using Dapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher05.Infrastructure
{
    public class DetailIncreaseVoucherRepository : BaseRepository<DetailIncreaseVoucher>, IDetailIncreaseVoucherRepository
    {
        public DetailIncreaseVoucherRepository(IUnitOfWork unitOfWork, IAutoIdRepository autoIdRepository, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, autoIdRepository, filterObjectHandler)
        {
        }

        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created by: vtahoang (24/08/2023) 
        public override string TableName { get; protected set; } = "asset_increase_voucher_detail";

        /// <summary>
        /// Tên cột id
        /// </summary>
        /// Created by: vtahoang (27/08/2023) 
        public override string TableId { get; protected set; } = "ref_detail_id";


        /// <summary>
        ///  Xoá nhiều bản ghi theo trường fieldId
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết</param>
        /// <param name="fieldId">Trường xoá theo</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        public async Task DeleteManyAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers, string fieldId)

        {
            var sql = $"Proc_asset_increase_voucher_detail_Delete";

            var listIds = detailIncreaseVouchers.Select(x => x.GetType().GetProperty(fieldId)?.GetValue(x, null));



            var listIdsString = "(" + string.Join(",", listIds.Select(x => "'" + x + "'")) + ")";

            var param = new DynamicParameters();

            param.Add("$field", fieldId);
            param.Add("$ListIds", listIdsString);

            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy danh sách chi tiết theo danh sách mã tài sản
        /// </summary>
        /// <param name="ids">Danh sách mã tài sản</param>
        /// <returns>Danh sách chi tiết</returns>
        /// Created by: vtahoang (29/08/2023) 
        public async Task<IEnumerable<DetailIncreaseVoucher>> GetDetailIncreaseVouchersByFixedAssetIds(List<Guid> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE fixed_asset_id in @listIds;";

            var param = new DynamicParameters();
            param.Add("listIds", ids);

            var detailIncreaseVouchers = await _uow.Connection.QueryAsync<DetailIncreaseVoucher>(sql, param, transaction: _uow.Transaction);

            return detailIncreaseVouchers;
        }

        /// <summary>
        /// Lấy danh sách chi tiết theo mã chứng từ
        /// </summary>
        /// <param name="masterId">Mã chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task<IEnumerable<DetailIncreaseVoucher>> GetDetailIncreaseVouchersByMasterId(Guid masterId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE ref_id = @masterId ORDER BY fixed_asset_code ASC;";

            var param = new DynamicParameters();
            param.Add("masterId", masterId);

            var detailIncreaseVouchers = await _uow.Connection.QueryAsync<DetailIncreaseVoucher>(sql, param, transaction: _uow.Transaction);

            return detailIncreaseVouchers;
        }

        /// <summary>
        /// Sửa nhiều bản ghi theo mã tài sản
        /// </summary>
        /// <param name="detailIncreaseVouchers"></param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        public async Task UpdateManyAsyncByFixedAssetId(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers)
        {
            var properties = typeof(DetailIncreaseVoucher).GetProperties();
            var set = "";

            // Thêm các tên cột vào câu lệnh
            foreach (var property in properties)
            {
                if (property.Name == "created_date" || property.Name == "created_by" || property.Name == "ref_detail_id") continue;
                set += $"{property.Name} = @{property.Name}, ";
            }
            set = set.Remove(set.Length - 2, 2);

            var sql = $"UPDATE {TableName} SET {set} WHERE fixed_asset_id = @fixed_asset_id ";

            // Tạo danh sách các tham số
            var listParam = new List<DynamicParameters>();

            foreach (var entity in detailIncreaseVouchers)
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
