using TaskFlowAPI.Authentication;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class AuthService(AuthRepository authRepository, TokenService tokenService, RefreshTokenRepository refreshTokenRepository)
    {
        private readonly AuthRepository authRepository = authRepository;
        private readonly RefreshTokenRepository refreshTokenRepository = refreshTokenRepository;
        private readonly TokenService tokenService = tokenService;

        public async Task<User> GetUserAsync(string email)
        {
            User? usuarioBanco = await authRepository.GetUserAsync(email);

           
            return usuarioBanco;
        }
        public async Task CriarUserAsync(RegisterDto user)
        {
            User? userBanco = await GetUserAsync(user.Email);

            if(userBanco != null)
            {
                throw new Exception("Já existe um usuario com esse email.");
            }
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User userWithHash = new User
            {   
                Name = user.Name,
                Email = user.Email,
                Password = senhaHash,
            };

            await authRepository.CriarUserAsync(userWithHash);
        }

        public async Task<LoginResponse> VerificarLoginAsync(LoginDto loginDto)
        {
            var usuario = await GetUserAsync(loginDto.Email);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuario não existe no banco.");
            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password);

            if (!senhaCorreta)
            {
                throw new Exception("Senha inválida");
            }

            var token = tokenService.GenerateToken(usuario);
            var refreshToken = tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = usuario.Id,
                ExpirationDate = DateTime.UtcNow.AddDays(7)
            };

            await refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken

            };
        }


    }
}
