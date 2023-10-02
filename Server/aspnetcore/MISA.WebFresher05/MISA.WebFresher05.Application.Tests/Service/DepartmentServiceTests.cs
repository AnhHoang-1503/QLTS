using AutoMapper;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Infrastructure
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        public IDepartmentRepository departmentRepository { get; set; }
        public IMapper mapper { get; set; }
        public IDepartmenManager departmenManager { get; set; }
        public IUnitOfWork unitOfWork { get; set; }
        public DepartmentService departmentService { get; set; }

        [SetUp]
        public void SetUp()
        {
            departmentRepository = Substitute.For<IDepartmentRepository>();
            mapper = Substitute.For<IMapper>();
            departmenManager = Substitute.For<IDepartmenManager>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            departmentService = new DepartmentService(departmentRepository, mapper, departmenManager, unitOfWork);
        }

        /// <summary>
        /// Test với đầu vào là chuỗi rỗng
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_EmptyList_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();

            var expectedMessage = "Không được truyền danh sách rỗng.";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await departmentService.DeleteManyAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).RollBackAsync();
        }

        /// <summary>
        /// Test với đầu vào có một hoặc nhiều id không hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_List10Items_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();
            // Tạo 10 id 
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var expectedMessage = "Không thể xoá.";

            var departments = new List<Department>();
            // Tạo 9 phòng ban 
            for (int i = 0; i < 9; i++)
            {
                var newDepartment = new Department();
                departments.Add(newDepartment);
            }

            departmentRepository.GetListByIdsAsync(ids).Returns(departments);

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await departmentService.DeleteManyAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await departmentRepository.Received(1).GetListByIdsAsync(ids);

            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).RollBackAsync();
        }

        /// <summary>
        /// Test với đầu vào là chuỗi hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_List10Items_Success()
        {
            // Arrange
            var ids = new List<Guid>();
            // Tạo 10 id 
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var departments = new List<Department>();
            // Tạo 10 phòng ban 
            for (int i = 0; i < 10; i++)
            {
                var newDepartment = new Department();
                departments.Add(newDepartment);
            }

            departmentRepository.GetListByIdsAsync(ids).Returns(departments);

            // Act && Assert
            await departmentService.DeleteManyAsync(ids);

            await departmentRepository.Received(1).GetListByIdsAsync(ids);

            await departmentRepository.Received(1).DeleteManyAsync(departments);

            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).CommitAsync();
        }
    }
}
