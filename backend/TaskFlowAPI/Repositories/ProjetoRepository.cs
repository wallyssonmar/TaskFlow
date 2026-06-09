using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class ProjetoRepository(TaskFlowApiContext taskFlowContext)
    {
        private readonly TaskFlowApiContext taskFlowContext = taskFlowContext;
        
        public async Task<Projeto?> ObterProjetoPorId(int id)
        {
             return await taskFlowContext.Projetos.FindAsync(id);
            
        }

        public async Task<List<Projeto>> GetProjetoAsync()
        {
           List<Projeto> projetos = await taskFlowContext.Projetos.ToListAsync();
           return projetos;
        }

        public async Task<Projeto> SetProjetoAsync(Projeto projeto)
        {
            EntityEntry<Projeto> retornoProjeto = await taskFlowContext.Projetos.AddAsync(projeto);
            await taskFlowContext.SaveChangesAsync();

            return retornoProjeto.Entity;
        }

        public async Task DeletarProjetoAsync(Projeto projeto)
        {
            taskFlowContext.Projetos.Remove(projeto);
            await taskFlowContext.SaveChangesAsync();
        }

        public async Task AtualizarProjeto()
        {
            
            await taskFlowContext.SaveChangesAsync();
        }

        public async Task<UserProjeto> SetUserProjeto(UserProjeto userProjeto)
        {
            EntityEntry<UserProjeto> retornoUserProjeto = await taskFlowContext.UserProjetos.AddAsync(userProjeto);
            await taskFlowContext.SaveChangesAsync();
            return retornoUserProjeto.Entity;
            
        }
    }
}
