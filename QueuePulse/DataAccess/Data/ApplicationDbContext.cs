using Microsoft.EntityFrameworkCore;
using QueuePulse.FluentAPIConfig;
using QueuePulse.Models.Entities;
namespace QueuePulse.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<QueueService> QueueServices { get; set; }
        public DbSet<QueueGroup> QueueGroups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new QueueServiceConfig());
            modelBuilder.ApplyConfiguration(new QueueGroupConfig());
        }
    }
}
