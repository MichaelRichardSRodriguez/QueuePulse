﻿using Microsoft.EntityFrameworkCore;
using QueuePulse.FluentAPIConfig;
using QueuePulse.Models;
namespace QueuePulse.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
        }
    }
}
