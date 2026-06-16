using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class RefreshTokenRepository(TaskFlowApiContext taskFlowApiContext)
    {
        private readonly TaskFlowApiContext taskFlowApiContext = taskFlowApiContext;

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            return await taskFlowApiContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
             
        }

        public async Task AddAsync(RefreshToken refreshTokenEntity)
        {
            taskFlowApiContext.RefreshTokens.Add(refreshTokenEntity);
            await taskFlowApiContext.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshTokenBanco)
        {
            await taskFlowApiContext.SaveChangesAsync();
        }

        internal async Task<RefreshToken?> GetRefreshTokenByUserAsync(int userId)
        {
            RefreshToken? refreshTokenBanco = await taskFlowApiContext.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == userId);
            return refreshTokenBanco;
        }

        internal async Task LogoutAsync(RefreshToken refreshTokenBanco)
        {
            taskFlowApiContext.RefreshTokens.Remove(refreshTokenBanco);
            await taskFlowApiContext.SaveChangesAsync();
        }
    }
}
