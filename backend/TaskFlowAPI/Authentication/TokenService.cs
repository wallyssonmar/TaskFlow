using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Authentication
{
    public class TokenService(IConfiguration configuration, RefreshTokenRepository refreshTokenRepository, AuthRepository authRepository)
    {
        private readonly IConfiguration configuration = configuration;
        private readonly RefreshTokenRepository refreshTokenRepository = refreshTokenRepository;
        private readonly AuthRepository authRepository = authRepository;


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

        public async Task SetRefreshTokenAsync(string refreshToken, int userId)
        {
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                ExpirationDate = DateTime.UtcNow.AddDays(7)
            };

            await refreshTokenRepository.AddAsync(refreshTokenEntity);
        }


        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()));
            ci.AddClaim(new Claim(type: ClaimTypes.Email, value: user.Email));
            ci.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Name));
            return ci;
        }

        public async Task<(bool isValid, string? userId)> ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return (false, null);

            var tokenParameters = TokenHelpers.GetTokenValidationParameters(configuration);

            var result = await new JwtSecurityTokenHandler()
                .ValidateTokenAsync(token, tokenParameters);

            if (!result.IsValid)
                return (false, null);

            var userId = result.Claims
                .FirstOrDefault(c => c.Key == ClaimTypes.NameIdentifier)
                .Value?.ToString();

            return (true, userId);
        }

        public async Task<LoginResponse?> ReNewRefreshToken(string refreshToken)
        {
            var refreshTokenBanco = await refreshTokenRepository.GetRefreshTokenAsync(refreshToken);

            if(refreshTokenBanco == null || refreshTokenBanco.ExpirationDate < DateTime.UtcNow)
            {
                return (null);
            }

            var user = await authRepository.GetUserByIdAsync(refreshTokenBanco.UserId);

            if(user is null)
            {
                return null;
            }

            var newToken = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            refreshTokenBanco.Token = newRefreshToken;
            refreshTokenBanco.ExpirationDate = DateTime.UtcNow.AddDays(7);

            await refreshTokenRepository.UpdateRefreshTokenAsync(refreshTokenBanco);

            return new LoginResponse 
            { 
                Token = newToken, 
                RefreshToken = newRefreshToken 
            };
        }
    }
}
