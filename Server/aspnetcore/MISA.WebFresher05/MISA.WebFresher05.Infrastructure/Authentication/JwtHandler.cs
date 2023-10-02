using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Infrastructure.Authentication;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MISA.WebFresher05.Infrastructure
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;

        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Tạo token jwt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public string Generate(User user)
        {
            // Định nghĩa dữ liệu payload (claim)
            var claims = new List<Claim>
            {
                new Claim("id", user.user_id ?? ""),
                new Claim("email", user.user_email ?? ""),
                new Claim("phone", user.user_phone ?? ""),
                new Claim("name", user.user_name ?? ""),
                new Claim("role", "user"),
                new Claim("exp", DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds().ToString())
            };

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Thời hạn của token
            var expires = DateTime.Now.AddDays(1);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            // Chuyển token thành chuỗi
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        /// <summary>
        /// Kiểm tra token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// Created by: vtahoang (16/09/2023) 
        public Task Verify(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            try
            {
                var claims = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                // Token không hợp lệ
                Console.WriteLine("Invalid token: " + ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
