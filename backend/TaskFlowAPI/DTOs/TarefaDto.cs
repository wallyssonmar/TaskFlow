namespace TaskFlowAPI.DTOs
{
    public class TarefaDto
    {
        public int id { get; set; }
        public int projetoId { get; set; }
        public required string name { get; set; }
        public required string description { get; set; }
        public required string prioridade { get; set; }
        public required string status { get; set; }
    }
}
