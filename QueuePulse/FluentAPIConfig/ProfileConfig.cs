using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.FluentAPIConfig
{
    public class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Status).HasMaxLength(10).HasDefaultValue(StaticDetails.CONTENTSTATUS_ACTIVE);
            builder.HasOne(d => d.Department).WithMany(p => p.Profiles).HasForeignKey(p => p.DepartmentId);

        }
    }
}
