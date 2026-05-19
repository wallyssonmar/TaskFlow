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
    }
}
