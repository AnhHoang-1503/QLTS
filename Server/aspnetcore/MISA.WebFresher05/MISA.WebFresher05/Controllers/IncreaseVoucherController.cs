using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Controllers.Base;
using System.Drawing.Text;

namespace MISA.WebFresher05.Controllers
{
    /// <summary>
    /// Controller cho chứng từ ghi tăng
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IncreaseVoucherController : BaseController<IncreaseVoucherDto, IncreaseVoucherCreateDto, IncreaseVoucherUpdateDto>
    {
        private readonly IIncreaseVoucherService _increaseVoucherService;

        public IncreaseVoucherController(IIncreaseVoucherService increaseVoucherService) : base(increaseVoucherService)
        {
            _increaseVoucherService = increaseVoucherService;
        }

        /// <summary>
        /// Lấy chi tiết chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDetailsById(Guid id)
        {
            var details = await _increaseVoucherService.GetDetailsAsync(id);

            return StatusCode(StatusCodes.Status200OK, details);
        }

        /// <summary>
        /// Trả về mã code mặc định
        /// </summary>
        /// <returns>Mã tài sản mặc định</returns>
        /// Created by: vtahoang (19/07/2023) 
        [HttpGet("default-code")]
        public async Task<IActionResult> DefaultCodeAsync()
        {
            var defaultCode = await _increaseVoucherService.GetDefaultCode();

            return StatusCode(StatusCodes.Status200OK, defaultCode);
        }
    }
}
