namespace TaskFlowAPI.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Color { get; set; }

        public List<Tarefa> Tarefas { get; set; } = [];

        public List<UserProjeto> UserProjetos { get; set; } = new();
    }
}
