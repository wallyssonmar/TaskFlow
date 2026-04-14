using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class ProjetoRepository(TaskFlowApiContext context)
    {
        private readonly TaskFlowApiContext context = context;
        
        public async Task<Projeto> ObterProjetoPorId(int id)
        {
            Projeto? projetoPorId = await context.Projetos.FindAsync(id);
            if (projetoPorId is null)
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");

            return projetoPorId;
        }

        public async Task<List<Projeto>> GetProjetoAsync()
        {
           List<Projeto> projetos = await context.Projetos.ToListAsync();
           return projetos;
        }

        public async Task<Projeto> SetProjetoAsync(Projeto projeto)
        {
            EntityEntry<Projeto> retornoProjeto = await context.Projetos.AddAsync(projeto);
            await context.SaveChangesAsync();

            return retornoProjeto.Entity;
        }

        public async Task DeletarProjetoAsync(Projeto projeto)
        {
            context.Projetos.Remove(projeto);
            await context.SaveChangesAsync();
        }

        public async Task AtualizarProjeto(Projeto projetoPorId, Projeto projeto)
        {
            context.Entry(projetoPorId).CurrentValues.SetValues(projeto);

            await context.SaveChangesAsync();
        }
    }
}
