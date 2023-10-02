using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Interface xử lý nghiệp vụ với tài sản
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public interface IFixedAssetExcelHandler : IBaseExcelHandler<FixedAssetDto, FixedAssetCreateDto>
    {
        /// <summary>
        /// Chuyển dữ liệu file excel thành list CreateDto
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: vtahoang (08/08/2023) 
        public Task<IEnumerable<FixedAssetCreateDto>> GetDataFromExcel(byte[] fileBytes);
    }
}
