namespace TaskFlowAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public required string name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }

        public List<UserProjeto> userProjetos { get; set; }

    }
}
