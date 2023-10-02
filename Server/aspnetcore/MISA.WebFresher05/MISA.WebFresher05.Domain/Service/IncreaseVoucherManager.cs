using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public class IncreaseVoucherManager : IIncreaseVoucherManager
    {
        private readonly IIncreaseVoucherRepository _increaseVoucherRepository;

        public IncreaseVoucherManager(IIncreaseVoucherRepository increaseVoucherRepository)
        {
            _increaseVoucherRepository = increaseVoucherRepository;
        }

        /// <summary>
        /// Kiểm tra mã chứng từ đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task CheckCodeExitsAsync(string code)
        {
            var count = await _increaseVoucherRepository.CountByCodeOrId(code, Guid.Empty);
            if (count > 0)
            {
                throw new ConflictException(String.Format(Resources.ResourcesVI.ExitsVoucherCode, code));
            }
        }

        /// <summary>
        /// Kiểm tra mã chứng từ đã tồn tại chưa cho update
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <param name="id">Id chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task CheckCodeExitsForUpdateAsync(string code, Guid id)
        {
            var count = await _increaseVoucherRepository.CountByCodeOrId(code, id);
            if (count > 1)
            {
                throw new ConflictException(String.Format(Resources.ResourcesVI.ExitsVoucherCode, code));
            }
        }
    }
}
