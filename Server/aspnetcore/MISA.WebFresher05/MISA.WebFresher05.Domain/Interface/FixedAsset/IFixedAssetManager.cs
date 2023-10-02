using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface quản lý dữ liệu tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public interface IFixedAssetManager
    {
        /// <summary>
        /// Kiểm tra xem mã tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        Task CheckFixedAssetExitsByCodeAsync(string code);

        /// <summary>
        /// Kiểm tra dữ liệu tài sản đã hợp lệ chưa
        /// </summary>
        /// <param name="fixedAsset">Tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        Task ValidateData(FixedAsset fixedAsset);

        /// <summary>
        /// Kiểm tra danh sách mã tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="codes">Danh sách mã tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        Task CheckListFixedAssetExitsByCodeAsync(List<string> codes);

        /// <summary>
        /// Kiểm tra mã tài sản mới đã tồn tại chưa khi cập nhật
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns></returns>
        Task CheckFixedAssetCodeExitsByCodeForUpdateAsync(string code, Guid fixedAssetId);

        /// <summary>
        /// Kiểm tra xem danh sách tài sản đã được ghi tăng chưa
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (11/09/2023) 
        Task CheckFixedAssetsIncreased(IEnumerable<FixedAsset> fixedAssets);
    }
}
