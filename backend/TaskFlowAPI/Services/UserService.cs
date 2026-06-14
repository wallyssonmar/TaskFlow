using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class UserService(UserRepository userRepository)
    {
        private readonly UserRepository _userRepository = userRepository;
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            User? userBanco = await _userRepository.GetUserByIdAsync(userId);
            if (userBanco is null)
                throw new KeyNotFoundException($"Usuario não existe no banco.");

            return new UserDto
            {
                Email = userBanco.Email,
                Name = userBanco.Name,
            };
        }
    }
}
