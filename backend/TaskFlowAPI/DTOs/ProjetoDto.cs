using TaskFlowAPI.Models;

namespace TaskFlowAPI.DTOs
{
    public class ProjetoDto
    {
        public int id {  get; set; }
        public required string name { get; set; }
        public required string description { get; set; }
        public required string color { get; set; }

        public List<Tarefa> tarefas { get; set; } = [];

        public List<UserProjeto> userProjetos { get; set; } = new();
    }
}
