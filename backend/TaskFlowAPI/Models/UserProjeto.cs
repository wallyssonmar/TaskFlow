namespace TaskFlowAPI.Models
{
    public class UserProjeto
    {
        public int User_Id { get; set; }
        public User? User { get; set; }
        public int Projeto_Id { get; set; }

        public Projeto? Projeto { get; set; }
    }
}
