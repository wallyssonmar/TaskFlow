using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskFlowAPI.Data;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repositories
{
    public class TarefaRepository(TaskFlowApiContext taskFlowApiContext)

    {
        private readonly TaskFlowApiContext taskFlowApiContext = taskFlowApiContext;


        public async Task<List<Tarefa>> PegarTarefasPorStatus(string status, int projetoId)
        {
            return await taskFlowApiContext.Tarefas.Where(t => t.status == status && t.projetoId == projetoId).ToListAsync();

        }

        public async Task<Tarefa> SetTarefaAsync(Tarefa tarefa)
        {
            EntityEntry<Tarefa> tarefaRetorno = await taskFlowApiContext.AddAsync(tarefa);
            await taskFlowApiContext.SaveChangesAsync();

            return tarefaRetorno.Entity;
            
        }
    }
}
