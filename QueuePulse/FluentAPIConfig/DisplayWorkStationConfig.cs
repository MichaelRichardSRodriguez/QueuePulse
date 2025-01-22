using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.FluentAPIConfig
{
	public class DisplayWorkStationConfig : IEntityTypeConfiguration<DisplayWorkstation>
	{
		public void Configure(EntityTypeBuilder<DisplayWorkstation> builder)
		{
			builder.HasKey(d => d.Id);
			builder.Property(d => d.CurrentQueue).HasMaxLength(10);
			builder.Property(d => d.Status).HasMaxLength(10).HasDefaultValue(StaticDetails.DISPLAY_OFFLINE);
			//builder.HasOne(p => p.Profile).WithOne(d => d.DisplayWorkStation).HasForeignKey(p => p.ProfileId);
		}
	}
}
