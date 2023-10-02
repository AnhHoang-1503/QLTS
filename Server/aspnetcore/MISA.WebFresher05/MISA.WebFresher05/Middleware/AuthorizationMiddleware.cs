using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;
using MISA.WebFresher05.Infrastructure.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace MISA.WebFresher05.Middleware
{
    /// <summary>
    /// Middleware xử lý authorization
    /// </summary>
    /// Created by: vtahoang (16/09/2023) 
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly JwtOptions _jwtOptions;

        public AuthorizationMiddleware(RequestDelegate next, IOptions<JwtOptions> jwtOptions)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Nếu endpoint chứa attribute [SkipAuthorization] thì bỏ qua
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    var skipAuthorization = endpoint.Metadata.OfType<SkipAuthorizationAttribute>().Any();
                    if (skipAuthorization)
                    {
                        await _next(context);
                        return;
                    }
                }

                var jwtToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                // Không có token
                if (string.IsNullOrEmpty(jwtToken))
                {
                    throw new InvalidTokenException(ResourcesVI.NullToken);
                }

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

                try
                {
                    // Kiểm tra token
                    var claims = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
                }
                catch
                {
                    throw new InvalidTokenException(ResourcesVI.InvalidToken);
                }

                await _next(context);
            }
            catch
            {
                throw;

            }
        }
    }
}
