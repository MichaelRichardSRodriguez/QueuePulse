using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueuePulse.Models.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? TicketNo { get; set; }
        public int ServiceId { get; set; }
        [DisplayName("Date Created")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Date Processed")]
        public DateTime ProcessedDate { get; set; }

        [DisplayName("Date Completed")]
        public DateTime CompletedDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("ServiceId")]
        [ValidateNever]
        public QueueService QueueService { get; set; }
    }
}
