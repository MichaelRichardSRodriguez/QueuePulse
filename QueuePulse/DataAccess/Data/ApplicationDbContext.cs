using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueuePulse.FluentAPIConfig;
using QueuePulse.Models.Entities;
namespace QueuePulse.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<QueueService> QueueServices { get; set; }
        public DbSet<QueueGroup> QueueGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new QueueServiceConfig());
            modelBuilder.ApplyConfiguration(new QueueGroupConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new ProfileConfig());
        }
    }
}
