namespace TaskFlowAPI.DTOs
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Prioridade { get; set; }
        public required string Status { get; set; }
    }
}
