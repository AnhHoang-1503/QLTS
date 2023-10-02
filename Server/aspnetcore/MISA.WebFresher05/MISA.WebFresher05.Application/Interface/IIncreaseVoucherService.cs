using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    public interface IIncreaseVoucherService : IBaseService<IncreaseVoucherDto, IncreaseVoucherCreateDto, IncreaseVoucherUpdateDto>
    {
        /// <summary>
        /// Lấy chi tiết ghi tăng
        /// </summary>
        /// <param name="refId">Định danh chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        Task<IEnumerable<DetailIncreaseVoucherDto>> GetDetailsAsync(Guid refId);

        /// <summary>
        /// Lấy mã sinh mặc định
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        Task<string?> GetDefaultCode();
    }
}
