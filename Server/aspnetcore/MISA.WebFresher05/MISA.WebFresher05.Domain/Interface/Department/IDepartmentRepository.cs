using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface repository của phòng ban
    /// </summary>
    /// Created by: vtahoang (08/08/2023) 
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Tìm phòng ban theo code
        /// </summary>
        /// <param name="code">Code phòng ban</param>
        /// <returns>Phòng ban có code tương ứng hoặc null</returns>
        /// Created by: vtahoang (18/07/2023) 
        Task<Department?> FindByCodeAsync(string code);
    }
}
