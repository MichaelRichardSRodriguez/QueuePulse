using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QueuePulse.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(10)]
        public string Description { get; set; }
        [DisplayName("Date Created")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Date Updated")]
        public DateTime UpdatedDate { get; set; }
        [DisplayName("Created By")]
        public string? CreatedBy { get; set; }
        [DisplayName("Updated By")]
        public string? UpdatedBy { get; set; }

        public string? Status { get; set; }

    }
}
