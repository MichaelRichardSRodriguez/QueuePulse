using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QueuePulse.Models
{
	public class QueueService
	{
        public int Id { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage ="Department Name is required.")]
        public int Department_Id { get; set; }
        [MinLength(5)]
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

        [ValidateNever]
        public Department Department { get; set; }

    }
}
