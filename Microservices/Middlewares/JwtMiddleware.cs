using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Libraries.Services;
using Microsoft.IdentityModel.Tokens;

namespace Microservices.Middlewares
{
    public class JwtMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly string? _jwtSecret;
        private readonly string? _encryptionKey;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _jwtSecret = configuration["JwtSettings:Key"];
            _encryptionKey = configuration["EncryptionKeys:DefaultKey"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                token = Utilities.Decrypt(token, _encryptionKey);
                TokenVerification(context, token);
            }
            await _next(context);
        }

        private void TokenVerification(HttpContext context, string? token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var claims = new ClaimsIdentity(jwtToken.Claims, "jwt");

                var principal = new ClaimsPrincipal(claims);
                context.User = principal;
            }
            catch (Exception) { }
        }
    }
}
