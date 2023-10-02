using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Entity của loại tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public class FixedAssetCategory : BaseAuditEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã code loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: vtahoang (21/07/2023) 
        public int? life_time { get; set; }

        /// <summary>
        /// Trả về mã
        /// </summary>
        /// <returns>Mã</returns>
        /// Created by: vtahoang (15/08/2023) 
        public string GetCode()
        {
            return fixed_asset_category_code ?? "";
        }

        /// <summary>
        /// Trả về id
        /// </summary>
        /// <returns>Id</returns>
        /// Created by: vtahoang (15/08/2023) 
        public Guid GetKey()
        {
            return fixed_asset_category_id;
        }
    }
}
