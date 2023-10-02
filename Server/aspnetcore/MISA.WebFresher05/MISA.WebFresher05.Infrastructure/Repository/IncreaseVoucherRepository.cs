using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    public class IncreaseVoucherRepository : BaseRepository<IncreaseVoucher>, IIncreaseVoucherRepository
    {
        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created by: vtahoang (27/08/2023) 
        public override string TableName { get; protected set; } = "asset_increase_voucher";

        /// <summary>
        /// Tên cột mã chứng từ
        /// </summary>
        /// Created by: vtahoang (29/08/2023) 
        public override string TableCode { get; protected set; } = "ref_no";

        /// <summary>
        /// Tên cột id
        /// </summary>
        /// Created by: vtahoang (27/08/2023) 
        public override string TableId { get; protected set; } = "ref_id";

        public IncreaseVoucherRepository(IUnitOfWork unitOfWork, IAutoIdRepository autoIdRepository, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, autoIdRepository, filterObjectHandler)
        {
        }
    }
}
