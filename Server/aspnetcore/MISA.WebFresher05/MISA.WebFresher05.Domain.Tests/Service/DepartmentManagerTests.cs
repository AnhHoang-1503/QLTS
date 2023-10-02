using MISA.WebFresher05.Infrastructure;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    [TestFixture]
    public class DepartmentManagerTests
    {
        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        [Test]
        public async Task CheckDepartmentExitsByCodeAsync_ExistDepartment_ConflicException()
        {
            // Arrange
            var code = "Hello";

            var departmentRepository = Substitute.For<IDepartmentRepository>();
            departmentRepository.FindByCodeAsync(code).Returns(new Department());
            var departmentManager = new DepartmentManager(departmentRepository);

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await departmentManager.CheckDepartmentExitsByCodeAsync(code));

            await departmentRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        [Test]
        public async Task CheckDepartmentExitsByCodeAsync_NotExistDepartment_Success()
        {
            // Arrange
            var code = "Hello";

            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var departmentManager = new DepartmentManager(departmentRepository);
            departmentRepository.FindByCodeAsync(code).ReturnsNull();

            // Act
            await departmentManager.CheckDepartmentExitsByCodeAsync(code);

            // Assert 
            await departmentRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
