using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Ngoại lệ chung
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public class BaseException
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public int ErrorCode { get; set; }

        /// <summary>
        /// Dev message
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public string? DevMessage { get; set; }

        /// <summary>
        /// User message
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public string? UserMessage { get; set; }

        /// <summary>
        /// Mã theo dõi
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public string? MoreInfor { get; set; }

        /// <summary>
        /// Lỗi
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public object? Errors { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

    }
}
