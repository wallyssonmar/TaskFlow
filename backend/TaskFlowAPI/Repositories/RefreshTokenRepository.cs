using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class RefreshTokenRepository(TaskFlowApiContext taskFlowApiContext)
    {
        private readonly TaskFlowApiContext taskFlowApiContext = taskFlowApiContext;
        public async Task AddAsync(RefreshToken refreshTokenEntity)
        {
            taskFlowApiContext.RefreshTokens.Add(refreshTokenEntity);
            await taskFlowApiContext.SaveChangesAsync();
        }
    }
}
