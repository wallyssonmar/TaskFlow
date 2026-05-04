using TaskFlowAPI.Models;

namespace TaskFlowAPI.DTOs
{
    public class TarefasPorStatusDto
    {
        public List<TarefaDto> Todo { get; set; } = new();
        public List<TarefaDto> InProgress { get; set; } = new();
        public List<TarefaDto> Done { get; set; } = new();
    }
}
