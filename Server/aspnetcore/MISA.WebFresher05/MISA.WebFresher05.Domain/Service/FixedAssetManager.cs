using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Quản lý nghiệp vụ tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)  
    public class FixedAssetManager : IFixedAssetManager
    {
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IDetailIncreaseVoucherRepository _detailIncreaseVoucherRepository;

        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository, IDetailIncreaseVoucherRepository detailIncreaseVoucherRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _detailIncreaseVoucherRepository = detailIncreaseVoucherRepository;
        }

        /// <summary>
        /// Kiểm tra mã tài sản mới đã tồn tại chưa khi cập nhật
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns></returns>
        public async Task CheckFixedAssetCodeExitsByCodeForUpdateAsync(string code, Guid fixedAssetId)
        {
            // Đếm số bản ghi có id và code trùng với dữ liệu truyền vào
            var count = await _fixedAssetRepository.CountByCodeOrId(code, fixedAssetId);

            // Trả về 1 bản ghi có id tương ứng, trả nếu code đã có trong db và khác code cũ sẽ trả về thêm bản ghi, khi đó thông báo lỗi
            if (count > 1)
            {
                throw new ConflictException(String.Format(Resources.ResourcesVI.ExistAssetCode, code));
            }

        }

        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu mã tài sản đã tồn tại.</exception>
        /// Created by: vtahoang (19/07/2023) 
        public async Task CheckFixedAssetExitsByCodeAsync(string code)
        {
            // Đếm số bản ghi có code tương ứng
            var count = await _fixedAssetRepository.CountByCodeOrId(code, Guid.Empty);

            if (count > 0)
            {
                throw new ConflictException(String.Format(Resources.ResourcesVI.ExistAssetCode, code));
            }
        }

        /// <summary>
        /// Kiểm tra xem danh sách tài sản đã được ghi tăng chưa
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (11/09/2023) 
        public async Task CheckFixedAssetsIncreased(IEnumerable<FixedAsset> fixedAssets)
        {
            var listIDs = fixedAssets.Select(asset => asset.fixed_asset_id).ToList();

            var detailIncreaseVoucher = await _detailIncreaseVoucherRepository.GetDetailIncreaseVouchersByFixedAssetIds(listIDs);

            // Danh sách mã tài sản đã ghi tăng
            var assetsCode = detailIncreaseVoucher.Select(detail => detail.fixed_asset_code).ToList();

            // Bỏ phần tử trùng lặp
            assetsCode = assetsCode.Distinct().ToList();

            // Danh sách chứng từ ghi tăng
            var voucherCode = detailIncreaseVoucher.Select(detail => detail.ref_no).ToList();

            // Bỏ phần tử trùng lặp
            voucherCode = voucherCode.Distinct().ToList();

            var listDetail = new { assetsCode = assetsCode, vouchersCode = voucherCode };

            var json = JsonSerializer.Serialize(listDetail);

            if (detailIncreaseVoucher.ToList().Count > 0)
            {
                throw new ConflictException(json);
            }
        }

        /// <summary>
        /// Kiểm tra danh sách mã tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="codes">Danh sách mã tài sản</param>
        /// <returns></returns>
        public async Task CheckListFixedAssetExitsByCodeAsync(List<string> codes)
        {
            var exitsAssets = await _fixedAssetRepository.FindByListCodeAsync(codes);

            // Danh sách mã tài sản đã tồn tại
            var exitsCodes = exitsAssets.Select(asset => asset.fixed_asset_code).ToList();
            // Bỏ phần tử trùng lặp
            exitsCodes = exitsCodes.Distinct().ToList();

            if (exitsCodes.Count > 0)
            {
                throw new ConflictException(String.Format(Resources.ResourcesVI.ExistAssetCode, string.Join(", ", exitsCodes)));
            }
        }

        /// <summary>
        /// Validate dữ liệu tài sản
        /// </summary>
        /// <param name="fixedAsset">Tài sản</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Created by: vtahoang (08/08/2023) 
        public Task ValidateData(FixedAsset fixedAsset)
        {
            if (fixedAsset.start_using_date?.Date < fixedAsset.purchase_date?.Date)
            {
                throw new InvalidDataException(Resources.ResourcesVI.UsingAfterBuyError);
            }

            var depreciationRateOdd = fixedAsset.depreciation_rate - (1.0 / fixedAsset.life_time) * 100;

            if (Math.Abs(depreciationRateOdd ?? 0) > 0.1)
            {
                throw new InvalidDataException(Resources.ResourcesVI.DepreciationRateError);
            }

            var depreciationValueYearOdd = fixedAsset.depreciation_value_year - fixedAsset.cost * (decimal)(fixedAsset.depreciation_rate ?? 0) / 100;

            if (Math.Abs(depreciationValueYearOdd ?? 0) > 10)
            {
                throw new InvalidDataException(Resources.ResourcesVI.DepreciationValueError);
            }

            return Task.CompletedTask;
        }


    }
}
