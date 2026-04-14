namespace TaskFlowAPI.Models
{
    public class Tarefa
    {
        public int id { get; set; }
        public Projeto projeto_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string prioridade { get; set; }
        public string status { get; set; }
    }
}
