namespace TaskFlowAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
    }
}
