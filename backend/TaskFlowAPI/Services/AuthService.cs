using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class AuthService(AuthRepository authRepository)
    {
        private readonly AuthRepository authRepository = authRepository;
        public async Task CriarUserAsync(UserDto user)
        {
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User userWithHash = new User
            {   
                Name = user.Name,
                Email = user.Email,
                Password = senhaHash,
            };

            await authRepository.CriarUserAsync(userWithHash);
        }
    }
}
