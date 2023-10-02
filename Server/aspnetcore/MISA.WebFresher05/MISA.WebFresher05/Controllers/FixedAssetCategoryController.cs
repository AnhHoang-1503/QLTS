using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher05.Controllers
{
    /// <summary>
    /// Controller cho loại tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoryController : BaseReadOnlyController<FixedAssetCategoryDto>
    {
        public FixedAssetCategoryController(IFixedAssetCategoryService fixedAssetCategoryService) : base(fixedAssetCategoryService)
        {
        }
    }
}
