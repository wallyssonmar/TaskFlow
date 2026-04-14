namespace TaskFlowAPI.Models
{
    public class UserProjeto
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public required User User { get; set; }
        public int projeto_id { get; set; }

        public required Projeto Projeto { get; set; }
    }
}
