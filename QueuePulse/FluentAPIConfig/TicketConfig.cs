using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;
using System.Reflection.Emit;

namespace QueuePulse.FluentAPIConfig
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TicketNo).IsRequired().HasMaxLength(10);
            builder.Property(t => t.Status).HasMaxLength(20).HasDefaultValue(StaticDetails.QUEUE_NEW);
            builder.HasOne(s => s.QueueService).WithMany(t => t.Ticket).HasForeignKey(t => t.ServiceId);

        }
    }
}
