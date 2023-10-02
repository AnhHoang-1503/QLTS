using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Quản lý nghiệp vụ của phòng ban
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public class DepartmentManager : IDepartmenManager
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Kiểm tra mã phòng ban đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu đã tồn tại</exception>
        /// Created by: vtahoang (19/07/2023) 
        public async Task CheckDepartmentExitsByCodeAsync(string code)
        {
            var department = await _departmentRepository.FindByCodeAsync(code);

            if (department != null)
            {
                throw new ConflictException(Resources.ResourcesVI.ExistDepartmentCode);
            }
        }
    }
}
