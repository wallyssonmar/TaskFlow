using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class UserRepository(TaskFlowApiContext taskFlowApiContext)
    {
        private readonly TaskFlowApiContext _taskFlowApiContext = taskFlowApiContext;
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _taskFlowApiContext.Users.FindAsync(userId);
        }
    }
}
