using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05
{
    public interface IDetailIncreaseVoucherRepository : IBaseRepository<DetailIncreaseVoucher>
    {
        /// <summary>
        /// Lấy danh sách chi tiết theo mã chứng từ
        /// </summary>
        /// <param name="masterId">Mã chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        Task<IEnumerable<DetailIncreaseVoucher>> GetDetailIncreaseVouchersByMasterId(Guid masterId);

        /// <summary>
        /// Sửa nhiều bản ghi theo mã tài sản
        /// </summary>
        /// <param name="detailIncreaseVouchers"></param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        Task UpdateManyAsyncByFixedAssetId(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers);

        /// <summary>
        /// Xoá nhiều bản ghi theo mã tài sản
        /// </summary>
        /// <param name="detailIncreaseVouchers"></param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        Task DeleteManyAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers, string fieldId);

        /// <summary>
        /// Lấy danh sách chi tiết theo danh sách mã tài sản
        /// </summary>
        /// <param name="ids">Danh sách mã tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (29/08/2023) 
        Task<IEnumerable<DetailIncreaseVoucher>> GetDetailIncreaseVouchersByFixedAssetIds(List<Guid> ids);
    }
}
