using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.FluentAPIConfig
{
    public class QueueGroupConfig : IEntityTypeConfiguration<QueueGroup>
	{
		public void Configure(EntityTypeBuilder<QueueGroup> builder)
		{
			builder.HasKey(d => d.Id);
			builder.Property(d => d.Name).IsRequired()
										.HasMaxLength(20);
			builder.Property(d => d.Description).IsRequired()
										.HasMaxLength(200);
			builder.Property(d => d.Status).HasMaxLength(10)
										.HasDefaultValue(StaticDetails.CONTENTSTATUS_ACTIVE);
		}
	}
}
