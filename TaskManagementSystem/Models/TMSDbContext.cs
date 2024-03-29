using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagementSystem.Models
{
    public class TMSDbContext : IdentityDbContext
    {
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options){}
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
