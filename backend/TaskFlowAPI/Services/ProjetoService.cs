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
        public async Task<List<Projeto>> GetProjetosAsync()
        {
            List<Projeto> projetos = await taskFlowRepository.GetProjetoAsync();
            return projetos;
        }

        public async Task<Projeto> SetProjetoAsync(Projeto projeto)
        {
            Projeto retornoProjeto = await taskFlowRepository.SetProjetoAsync(projeto);
            return retornoProjeto;
        }

        public async Task DeletarProjetoAsync(int id)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            if (projetoPorId is null)
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");
            await taskFlowRepository.DeletarProjetoAsync(projetoPorId);
        }

        public async Task AtulizarProjeto(int id, Projeto projeto)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            

            await taskFlowRepository.AtualizarProjeto(projetoPorId, projeto);
        }
    }
}
