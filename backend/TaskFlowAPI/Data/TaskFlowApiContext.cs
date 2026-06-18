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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProjeto>()
                .HasKey(up => new { up.User_Id, up.Projeto_Id });

            modelBuilder.Entity<UserProjeto>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjetos)
                .HasForeignKey(up => up.User_Id);

            modelBuilder.Entity<UserProjeto>()
                .HasOne(up => up.Projeto)
                .WithMany(p => p.UserProjetos)
                .HasForeignKey(up => up.Projeto_Id);
        }
    }
}
