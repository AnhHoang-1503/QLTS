using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    /// <summary>
    /// Repository của loại tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    public class FixedAssetCategoryRepository : BaseReadOnlyRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        /// <summary>
        /// Tên bảng trong database
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        public override string TableName { get; protected set; } = "fixed_asset_category";

        /// <summary>
        /// Tên cột id trong database
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        public override string TableId { get; protected set; } = "fixed_asset_category_id";

        public FixedAssetCategoryRepository(IUnitOfWork unitOfWork, IFilterObjectHandler filterObjectHandler) : base(unitOfWork, filterObjectHandler)
        {
        }
    }
}
