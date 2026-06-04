using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Authentication
{
    public class TokenService(IConfiguration configuration)
    {
        private readonly IConfiguration configuration = configuration;


        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JwtSettings:ExpirationMinutes"]!)),
                Issuer = configuration["JwtSettings:Issuer"],
                Audience = configuration["JwtSettings:Audience"],

            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }




        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()));
            ci.AddClaim(new Claim(type: ClaimTypes.Email, value: user.Email));
            ci.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Name));
            return ci;
        }
    }
}
