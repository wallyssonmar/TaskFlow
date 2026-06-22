namespace TaskFlowAPI.DTOs
{
    public class AtualizarTarefaDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Prioridade { get; set; }
    }
}
