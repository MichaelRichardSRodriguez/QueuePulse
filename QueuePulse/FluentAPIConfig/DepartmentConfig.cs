using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models;
using QueuePulse.Utility;
using System.Reflection.Emit;

namespace QueuePulse.FluentAPIConfig
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Status).HasMaxLength(10).HasDefaultValue(StaticDetails.ContentStatus_Active);

            builder.HasData(
                new Department
                {
                    Id = 1,
                    Name = "Admission",
                    Description = "Description",
                    CreatedDate = new DateTime(2024, 12, 30, 13, 0, 0),
                    CreatedBy = "MIKE",
                    UpdatedBy = null,
                    Status = StaticDetails.ContentStatus_Active
                },
                new Department
                {
                    Id = 2,
                    Name = "Billing",
                    Description = "Description",
                    CreatedDate = new DateTime(2024, 12, 30, 13, 0, 0),
                    CreatedBy = "MIKE",
                    UpdatedBy = null,
                    Status = StaticDetails.ContentStatus_Active
                }
            );
        }
    }
}
