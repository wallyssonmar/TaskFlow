using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class ProjetoService(ProjetoRepository taskFlowRepository)
    {
        private readonly ProjetoRepository taskFlowRepository = taskFlowRepository;
        
        public async Task<Projeto> ObterProjetoPorId(int id)
        {
            Projeto? projeto = await taskFlowRepository.ObterProjetoPorId(id);
            if (projeto == null)
            {
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");

            }
            return projeto;
        }
        public async Task<List<ProjetoDto>> GetProjetosAsync()
        {
            List<Projeto> projetos = await taskFlowRepository.GetProjetoAsync();
            List<ProjetoDto> projetoDtos = new List<ProjetoDto>();

            foreach (var projeto in projetos)
            {
                projetoDtos.Add(new ProjetoDto
                {
                    id = projeto.id,
                    name = projeto.name,
                    description = projeto.description,
                    color = projeto.color,
                });
                   
            }
            return projetoDtos;
        }

        public async Task<ProjetoDto> GetProjetoByIdAsync(int id)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            

            return new ProjetoDto
            {
                id = projetoPorId.id,
                name = projetoPorId.name,
                description = projetoPorId.description,
                color = projetoPorId.color,

            };
        }

        public async Task<ProjetoDto> SetProjetoAsync(ProjetoDto projetodto)
        {
            Projeto projeto = new Projeto
            {
                
                name = projetodto.name,
                description = projetodto.description,
                color = projetodto.color,

            };
            Projeto retornoProjeto = await taskFlowRepository.SetProjetoAsync(projeto);


            return new ProjetoDto
            {   
                id = retornoProjeto.id,
                name = retornoProjeto.name,
                description = retornoProjeto.description,
                color = retornoProjeto.color,
            };
        }

        public async Task DeletarProjetoAsync(int id)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            if (projetoPorId is null)
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");
            await taskFlowRepository.DeletarProjetoAsync(projetoPorId);
        }

        public async Task AtulizarProjeto(int id, ProjetoUpdateDto projetoDto)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);


            projetoPorId.name = projetoDto.name;
            projetoPorId.description = projetoDto.description;
            projetoPorId.color = projetoDto.color;
            

            await taskFlowRepository.AtualizarProjeto();
        }
    }
}
