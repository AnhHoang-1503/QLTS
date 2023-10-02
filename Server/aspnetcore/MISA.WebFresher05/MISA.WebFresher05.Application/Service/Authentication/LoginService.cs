
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;
using System.Reflection;

namespace MISA.WebFresher05.Application
{
    public class LoginService : ILoginService
    {
        private readonly IJwtHandler _jwtProvider;

        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher _passwordHasher;

        public LoginService(IJwtHandler jwtProvider, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Xử lý login
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public async Task<string> LoginAsync(LoginRequest request)
        {
            if (request.Account is null)
            {
                throw new NotFoundException(ResourcesVI.NotFoundUser);
            }

            // Lấy thông tin user
            var user = await _userRepository.GetUserAsync(request.Account);

            if (user is null)
            {
                throw new NotFoundException(ResourcesVI.NotFoundUser);
            }

            // Kiểm tra mật khẩu
            var verified = _passwordHasher.Compare(request.Password ?? "", user.password ?? "");
            if (!verified)
            {
                throw new NotFoundException(ResourcesVI.NotFoundUser);
            }

            // Tạo token
            var token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
