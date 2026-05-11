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
                    Id = projeto.Id,
                    Name = projeto.Name,
                    Description = projeto.Description,
                    Color = projeto.Color,
                });
                   
            }
            return projetoDtos;
        }

        public async Task<ProjetoDto> GetProjetoByIdAsync(int id)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            

            return new ProjetoDto
            {
                Id = projetoPorId.Id,
                Name = projetoPorId.Name,
                Description = projetoPorId.Description,
                Color = projetoPorId.Color,

            };
        }

        public async Task<ProjetoDto> SetProjetoAsync(ProjetoDto projetodto)
        {
            Projeto projeto = new Projeto
            {
                
                Name = projetodto.Name,
                Description = projetodto.Description,
                Color = projetodto.Color,

            };
            Projeto retornoProjeto = await taskFlowRepository.SetProjetoAsync(projeto);


            return new ProjetoDto
            {   
                Id = retornoProjeto.Id,
                Name = retornoProjeto.Name,
                Description = retornoProjeto.Description,
                Color = retornoProjeto.Color,
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


            projetoPorId.Name = projetoDto.Name;
            projetoPorId.Description = projetoDto.Description;
            projetoPorId.Color = projetoDto.Color;
            

            await taskFlowRepository.AtualizarProjeto();
        }
    }
}
