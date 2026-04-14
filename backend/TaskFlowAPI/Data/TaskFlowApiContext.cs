using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Data
{
    public class TaskFlowApiContext : DbContext
    {
        public TaskFlowApiContext(DbContextOptions<TaskFlowApiContext> opt) : base(opt) { }

        
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProjeto> UserProjetos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProjeto>()
                .HasKey(up => new { up.user_id, up.projeto_id });
        }
    }
}
