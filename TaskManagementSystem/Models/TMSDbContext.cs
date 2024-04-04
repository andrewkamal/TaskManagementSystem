using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagementSystem.Models
{
    public class TMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options){}
        public DbSet<Tasks> Task { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTeam>().HasKey(u => new { u.UserId, u.TeamId });
        }
    }
}
