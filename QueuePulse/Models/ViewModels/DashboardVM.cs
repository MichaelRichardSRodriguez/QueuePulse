using QueuePulse.Models.Entities;

namespace QueuePulse.Models.ViewModels
{
    public class DashboardVM
    {
        public List<Department> Departments { get; set; }
        public List<QueueService> QueueServices { get; set; }
        public TicketStat TicketStats { get; set; }  // This will hold ticket status counts.
    }
}
