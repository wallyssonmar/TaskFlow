using System.Reflection.Metadata.Ecma335;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class TarefaService(TarefaRepository tarefaRepository)
    {
        private readonly TarefaRepository tarefaRepository = tarefaRepository;
        public async Task<TarefasPorStatusDto> ListarTarefasPorStatus(int id)
        {
            var todo = await tarefaRepository.PegarTarefasPorStatus("A Fazer", id);
            var progress = await tarefaRepository.PegarTarefasPorStatus("Em progresso",id);
            var done = await tarefaRepository.PegarTarefasPorStatus("Concluído", id);


            var response = new TarefasPorStatusDto
            {
                Todo = todo.Select(t => new TarefaDto
                {
                    id = t.id,
                    name = t.name,
                    description = t.description,
                    prioridade = t.prioridade,
                    status = t.status,
                }).ToList(),
                InProgress = progress.Select(t => new TarefaDto
                {
                    id = t.id,
                    name = t.name,
                    description= t.description,
                    prioridade = t.prioridade,
                    status = t.status,
                }).ToList(),
                Done = done.Select(t => new TarefaDto
                {
                    id = t.id,
                    name = t.name,
                    description = t.description,
                    prioridade = t.prioridade,
                    status = t.status,
                }).ToList()
            };

            return response;
        }

        public async Task<TarefaDto> SetTarefaAsync(TarefaDto tarefaDto)
        {
            Tarefa tarefa = new Tarefa
            {   
                projetoId = tarefaDto.projetoId,
                name = tarefaDto.name,
                description = tarefaDto.description,
                status = tarefaDto.status,
                prioridade = tarefaDto.prioridade
            };

            Tarefa tarefaRetorno = await tarefaRepository.SetTarefaAsync(tarefa);

            TarefaDto tarefaDtoRetorno = new TarefaDto
            {
                projetoId = tarefaRetorno.projetoId,
                name = tarefaRetorno.name,
                description = tarefaRetorno.description,
                status = tarefaRetorno.status,
                prioridade = tarefaRetorno.prioridade
            };
            return tarefaDto;
        }


    }
}
