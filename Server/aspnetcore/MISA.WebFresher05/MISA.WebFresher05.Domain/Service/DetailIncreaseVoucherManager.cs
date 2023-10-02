using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public class DetailIncreaseVoucherManager : IDetailIncreaseVoucherManager
    {
        private readonly IDetailIncreaseVoucherRepository _detailIncreaseVoucherRepository;

        public DetailIncreaseVoucherManager(IDetailIncreaseVoucherRepository detailIncreaseVoucherRepository)
        {
            _detailIncreaseVoucherRepository = detailIncreaseVoucherRepository;
        }

        /// <summary>
        /// Kiểm tra danh sách tài sản đã có chứng từ chưa
        /// </summary>
        /// <param name="ids">Danh sách id tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        public async Task CheckExistListFixedAsset(List<Guid> ids)
        {
            var fixedAssets = await _detailIncreaseVoucherRepository.GetDetailIncreaseVouchersByFixedAssetIds(ids);

            var litsExist = fixedAssets.ToList().Select(x => x.fixed_asset_code).ToList();

            if (litsExist.Any())
            {
                throw new InvalidDataException(String.Format(Resources.ResourcesVI.AlreadyIncrease, string.Join(", ", litsExist))
                   );
            }
        }
    }

}
