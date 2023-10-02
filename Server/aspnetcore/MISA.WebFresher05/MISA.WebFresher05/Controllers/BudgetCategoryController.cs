using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;

namespace MISA.WebFresher05.Controllers
{
    /// <summary>
    /// Controller nguồn hình thành
    /// </summary>
    /// Created by: vtahoang (24/08/2023) 
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BudgetCategoryController : BaseReadOnlyController<BudgetCategoryDto>
    {
        public BudgetCategoryController(IBudgetCategoryService budgetCategoryService) : base(budgetCategoryService)
        {
        }
    }
}
