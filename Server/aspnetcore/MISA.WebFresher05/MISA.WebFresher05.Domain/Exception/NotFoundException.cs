using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Lỗi không tìm thấy dữ liệu
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public class NotFoundException : Exception
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
        /// Khởi tạo lỗi không tìm thấy dữ liệu
        /// </summary>
        /// Created by: vtahoang (08/08/2023) 
        public NotFoundException() { }

        /// <summary>
        /// Khời tạo lỗi không tìm thấy dữ liệu với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message)
        {
            ErrorCode = 404;
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// Created by: vtahoang (08/08/2023) 
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
