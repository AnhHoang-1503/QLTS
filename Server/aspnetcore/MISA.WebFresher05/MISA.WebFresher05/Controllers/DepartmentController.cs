using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Controllers.Base;
using MySqlConnector;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher05.Controllers
{
    /// <summary>
    /// Controller cho phòng ban
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
