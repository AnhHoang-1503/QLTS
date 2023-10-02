using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    public interface IDetailIncreaseVoucherService : IBaseService<DetailIncreaseVoucherDto, DetailIncreaseVoucherCreateDto, DetailIncreaseVoucherUpdateDto>
    {
        /// <summary>
        /// Lấy chi tiết ghi tăng theo chứng từ
        /// </summary>
        /// <param name="masterId">Định danh chứng từ</param>
        /// <returns>Chi tiết</returns>
        /// Created by: vtahoang (24/08/2023) 
        Task<IEnumerable<DetailIncreaseVoucherDto>> GetDetailIncreaseVouchersByMasterId(Guid masterId);

        /// <summary>
        /// Tạo nhiều chi tiết ghi tăng
        /// </summary>
        /// <param name="detailIncreaseVoucher">Danh sách chi tiết ghi tăng</param>
        /// <returns></returns>
        /// Created by: vtahoang (29/08/2023) 
        Task InsertMany(IEnumerable<DetailIncreaseVoucher> detailIncreaseVoucher);

        /// <summary>
        /// Xoá nhiều chi tiết ghi tăng theo chứng từ
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (05/09/2023) 
        Task DeleteManyByMasterIdAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers);

        /// <summary>
        /// Xoá nhiều chi tiết ghi tăng theo mã tài sản
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (05/09/2023) 
        Task DeleteManyByFixedAssetIdAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers);
    }
}
