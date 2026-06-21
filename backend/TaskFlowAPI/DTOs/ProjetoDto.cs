using TaskFlowAPI.Models;

namespace TaskFlowAPI.DTOs
{
    public class ProjetoDto
    {
        public int Id {  get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Color { get; set; }
        public int QtdTarefa { get; set; }
        public int QtdTarefasConcluidas { get; set; }
    }
}
