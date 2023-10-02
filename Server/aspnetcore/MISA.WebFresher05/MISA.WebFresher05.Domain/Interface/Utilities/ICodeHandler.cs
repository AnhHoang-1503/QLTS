using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    public interface ICodeHandler
    {
        /// <summary>
        /// Tách mã thành các thành phần
        /// </summary>
        /// <param name="code">Mã</param>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        Task<AutoId> Split(string code);

        /// <summary>
        /// Tạo mã từ các thành phần
        /// </summary>
        /// <param name="autoId">Các thành phần của mã</param>
        /// <returns></returns>
        /// Created by: vtahoang (15/08/2023) 
        Task<string> GetCode(AutoId autoId);
    }
}
