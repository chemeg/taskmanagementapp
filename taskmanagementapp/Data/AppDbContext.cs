using Microsoft.EntityFrameworkCore;
using taskmanagementapp.Models;

namespace taskmanagementapp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Column> Columns { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TaskManagementApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }

            base.OnConfiguring(optionsBuilder);
            
        }
    }
}
