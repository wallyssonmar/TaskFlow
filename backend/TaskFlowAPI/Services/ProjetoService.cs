using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repositories;

namespace TaskFlowAPI.Services
{
    public class ProjetoService(ProjetoRepository projetoRepository, AuthRepository authRepository)
    {
        private readonly ProjetoRepository projetoRepository = projetoRepository;
        private readonly AuthRepository authRepository = authRepository;

        public async Task<Projeto> ObterProjetoPorId(int id)
        {
            Projeto? projeto = await projetoRepository.ObterProjetoPorId(id);
            if (projeto == null)
            {
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");

            }
            return projeto;
        }
        public async Task<List<ProjetoDto>> GetProjetosAsync(int userId)
        {
            var user = await authRepository.GetUserByIdAsync(userId);
            if (user is null)
                return null;


            List<Projeto> projetos = await projetoRepository.GetProjetoAsync(userId);
            Console.WriteLine(projetos[0].Tarefas.Count);

            return projetos.Select(projeto => new ProjetoDto
            {
                Id = projeto.Id,
                Name = projeto.Name,
                Description = projeto.Description,
                Color = projeto.Color,
                QtdTarefa = projeto.Tarefas.Count(),
                QtdTarefasConcluidas = projeto.Tarefas.Count(t => t.Status == "Concluído")

            }).ToList();
        }

        public async Task<ProjetoDto> GetProjetoByUserAsync(int id, int userId)
        {
            Projeto? projetoPorId = await projetoRepository.ObterProjetoPorUser(id,userId);
            if(projetoPorId is null)
                    throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");

            return new ProjetoDto
            {
                Id = projetoPorId.Id,
                Name = projetoPorId.Name,
                Description = projetoPorId.Description,
                Color = projetoPorId.Color,

            };
        }

        

        public async Task DeletarProjetoAsync(int id)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            if (projetoPorId is null)
                throw new KeyNotFoundException($"Registro com id {id} não existe no banco.");
            await projetoRepository.DeletarProjetoAsync(projetoPorId);
        }

        public async Task AtulizarProjeto(int id, ProjetoUpdateDto projetoDto)
        {
            Projeto projetoPorId = await ObterProjetoPorId(id);
            Projeto? verificarProjeto = await projetoRepository.GetProjetoPorNomeAsync(projetoDto.Name);
            if (verificarProjeto != null)
                throw new KeyNotFoundException($"Já existe projeto com esse nome.");

            projetoPorId.Name = projetoDto.Name;
            projetoPorId.Description = projetoDto.Description;
            projetoPorId.Color = projetoDto.Color;
            

            await projetoRepository.AtualizarProjeto();
        }

        public async Task<UserProjetoResponseDto> SetUserProjetoAsync(Projeto projeto, int userId)
        {
            User? user = await authRepository.GetUserByIdAsync(userId);
            if (user is null)
                throw new KeyNotFoundException($"Registro com id {user} não existe no banco.");

            Projeto? verificarProjeto = await projetoRepository.GetProjetoPorNomeAsync(projeto.Name);
            if(verificarProjeto != null)
                throw new KeyNotFoundException($"Já existe projeto com esse nome.");

            Projeto projetoBanco = await projetoRepository.SetProjetoAsync(projeto);

            UserProjeto userProjeto = new UserProjeto
            {
                User_Id = user.Id,
                User = user,
                Projeto_Id = projetoBanco.Id,
                Projeto = projetoBanco

            };

            UserProjeto userProjetoResponse = await projetoRepository.SetUserProjeto(userProjeto);

            return new UserProjetoResponseDto
            {
                UserId = userProjetoResponse.User_Id,
                ProjetoId = userProjetoResponse.Projeto_Id
            };
        }

        public async Task AddMembroProjetoAsync(MembroAddDto emailAdd, int idProjeto)
        {

            var userBanco = await authRepository.GetUserAsync(emailAdd.Email);
            if (userBanco is null)
                throw new KeyNotFoundException("Usuario nã oencontrado");

            UserProjeto userProjeto = new UserProjeto
            {
                User_Id = userBanco.Id,
                Projeto_Id = idProjeto
            };

            UserProjeto userProjetoResponse = await projetoRepository.SetUserProjeto(userProjeto);

        }
    }
}
