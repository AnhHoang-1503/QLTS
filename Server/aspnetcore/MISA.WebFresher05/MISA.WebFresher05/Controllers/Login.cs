using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;

namespace MISA.WebFresher05.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly ILoginService _loginService;

        public Login(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <returns>Token jwt</returns>
        /// Created by: vtahoang (16/09/2023) 
        [HttpPost]
        [SkipAuthorization]
        public async Task<IActionResult> LoginRequest([FromBody] LoginRequest request)
        {
            var token = await _loginService.LoginAsync(request);

            return StatusCode(StatusCodes.Status200OK, token);
        }

        /// <summary>
        /// Kiểm tra xem đã đăng nhập chưa
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        [HttpGet]
        public IActionResult Logged()
        {
            return StatusCode(StatusCodes.Status200OK, "OK");
        }
    }
}
