using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class AuthRepository(TaskFlowApiContext taskFlowApiContext)
    {
        private readonly TaskFlowApiContext taskFlowApiContext = taskFlowApiContext;
        internal async Task CriarUserAsync(User userWithHash)
        {
            taskFlowApiContext.Users.Add(userWithHash);
            await taskFlowApiContext.SaveChangesAsync();
        }

        internal async Task<User?> GetUserAsync(string email)
        {
            return await taskFlowApiContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await taskFlowApiContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
