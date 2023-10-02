using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Lỗi trùng lặp dữ liệu
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public class ConflictException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        #endregion

        #region Constructor
        public ConflictException() { }

        /// <summary>
        /// Khởi tạo lỗi trùng lặp dữ liệu với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// Created by: vtahoang (08/08/2023) 
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khời tạo lỗi trùng lặp dữ liệu với thông điệp
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// Created by: vtahoang (08/08/2023) 
        public ConflictException(string message) : base(message)
        {
            ErrorCode = 409;
        }

        /// <summary>
        /// Khởi tạo lỗi trùng lặp dữ liệu với thông điệp và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// Created by: vtahoang (08/08/2023) 
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
