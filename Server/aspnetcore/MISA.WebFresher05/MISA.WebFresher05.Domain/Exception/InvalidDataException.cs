using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Lỗi dữ liệu không hợp lệ
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public class InvalidDataException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public int ErrorCode { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo lỗi dữ liệu không hợp lệ
        /// </summary>
        public InvalidDataException() { }

        /// <summary>
        /// Khởi tạo lỗi dữ liệu không hợp lệ với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public InvalidDataException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo lỗi dữ liệu không hợp lệ với thông điệp
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// Created by: vtahoang (08/08/2023) 
        public InvalidDataException(string message) : base(message)
        {
            ErrorCode = 400;
        }

        /// <summary>
        /// Khời tạo lỗi dữ liệu không hợp lệ với thông điệp và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// <param name="errorCode">Mã lỗi</param> 
        /// Created by: vtahoang (08/08/2023) 
        public InvalidDataException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
