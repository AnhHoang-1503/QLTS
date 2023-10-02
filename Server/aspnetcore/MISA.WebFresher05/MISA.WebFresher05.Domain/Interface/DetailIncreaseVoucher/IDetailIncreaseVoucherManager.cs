using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public interface IDetailIncreaseVoucherManager
    {
        /// <summary>
        /// Kiểm tra danh sách tài sản đã có chứng từ chưa
        /// </summary>
        /// <param name="ids">Danh sách id tài sản</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        Task CheckExistListFixedAsset(List<Guid> ids);
    }
}
