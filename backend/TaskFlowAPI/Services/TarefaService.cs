using System.Reflection.Metadata.Ecma335;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class TarefaService(TarefaRepository tarefaRepository)
    {
        private readonly TarefaRepository tarefaRepository = tarefaRepository;

        public async Task<Tarefa> ObterTarefaPorId(int id)
        {
            Tarefa tarefaPorId = await tarefaRepository.PegarTarefaPorId(id);
            if (tarefaPorId == null)
                throw new Exception($"O registro com o id {id} não existe no banco");
            return tarefaPorId;
        }
        public async Task<TarefasPorStatusDto> ListarTarefasPorStatus(int id)
        {
            var todo = await tarefaRepository.PegarTarefasPorStatus("A Fazer", id);
            var progress = await tarefaRepository.PegarTarefasPorStatus("Em progresso",id);
            var done = await tarefaRepository.PegarTarefasPorStatus("Concluído", id);


            var response = new TarefasPorStatusDto
            {
                Todo = todo.Select(t => new TarefaDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Prioridade = t.Prioridade,
                    Status = t.Status,
                }).ToList(),
                InProgress = progress.Select(t => new TarefaDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description= t.Description,
                    Prioridade = t.Prioridade,
                    Status = t.Status,
                }).ToList(),
                Done = done.Select(t => new TarefaDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Prioridade = t.Prioridade,
                    Status = t.Status,
                }).ToList()
            };

            return response;
        }

        public async Task<TarefaDto> SetTarefaAsync(TarefaDto tarefaDto)
        {
            Tarefa tarefa = new Tarefa
            {   
                ProjetoId = tarefaDto.ProjetoId,
                Name = tarefaDto.Name,
                Description = tarefaDto.Description,
                Status = tarefaDto.Status,
                Prioridade = tarefaDto.Prioridade
            };

            Tarefa tarefaRetorno = await tarefaRepository.SetTarefaAsync(tarefa);

            TarefaDto tarefaDtoRetorno = new TarefaDto
            {
                ProjetoId = tarefaRetorno.ProjetoId,
                Name = tarefaRetorno.Name,
                Description = tarefaRetorno.Description,
                Status = tarefaRetorno.Status,
                Prioridade = tarefaRetorno.Prioridade
            };
            return tarefaDto;
        }

        public async Task ExcluirTarefaAsync(int idProjeto, int id)
        {
            Tarefa tarefaPorId = await ObterTarefaPorId(id);
            if(idProjeto != tarefaPorId.ProjetoId)
            {
                throw new KeyNotFoundException("Essa tarefa não pertence a esse projeto.");
            }
            await tarefaRepository.ExcluirTarefaPorId(tarefaPorId);
        }

        internal async Task AtualizarTarefaAsync(Tarefa tarefa, int idProjeto, int id)
        {
            Tarefa tarefaNoBanco = await ObterTarefaPorId(id);
            if (idProjeto != tarefaNoBanco.ProjetoId)
            {
                throw new KeyNotFoundException("Essa tarefa não pertence a esse projeto.");

            }


            tarefaNoBanco.Name = tarefa.Name;
            tarefaNoBanco.Description = tarefa.Description;
            tarefaNoBanco.Status = tarefa.Status;
            tarefaNoBanco.Prioridade = tarefa.Prioridade;
            

            
            await tarefaRepository.AtualizarTarefaAsync();
        }
    }
}
