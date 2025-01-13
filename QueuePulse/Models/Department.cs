using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QueuePulse.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MinLength(5, ErrorMessage = "Should have atleast 5 characters.")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters only.")]
        public string Name { get; set; }
        [MinLength(10,ErrorMessage ="Should have atleast 10 characters.")]
        [MaxLength(200, ErrorMessage = "Maximum of 200 characters only.")]
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

        [ValidateNever]
        public IEnumerable<QueueService> Services { get; set; }

    }
}
