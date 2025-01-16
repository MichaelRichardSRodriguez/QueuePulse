using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.FluentAPIConfig
{
    public class QueueServiceConfig : IEntityTypeConfiguration<QueueService>
	{
		public void Configure(EntityTypeBuilder<QueueService> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);
			builder.Property(x => x.Description)
					.IsRequired()
					.HasMaxLength(200);
			builder.Property(d => d.Status)
					.HasMaxLength(10)
					.HasDefaultValue(StaticDetails.CONTENTSTATUS_ACTIVE);
			builder.HasOne(x => x.Department)
					.WithMany(x => x.Services)
					.HasForeignKey(x => x.Department_Id)
					.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
