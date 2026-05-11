namespace TaskFlowAPI.Models
{
    public class UserProjeto
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public required User User { get; set; }
        public int Projeto_Id { get; set; }

        public required Projeto Projeto { get; set; }
    }
}
