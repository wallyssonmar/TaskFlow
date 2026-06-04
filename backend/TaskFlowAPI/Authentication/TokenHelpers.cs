using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskFlowAPI.Authentication
{
    public static class TokenHelpers
    {
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {

            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);

            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            
        }
    }
}
