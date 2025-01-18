using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QueuePulse.Models.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Counter Name")]
        [MinLength(1, ErrorMessage = "Should have atleast 5 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum of 50 characters only.")]
        public string Name { get; set; }

        [DisplayName("Date Created")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Date Updated")]
        public DateTime UpdatedDate { get; set; }
        [DisplayName("Created By")]
        public string? CreatedBy { get; set; }
        [DisplayName("Updated By")]
        public string? UpdatedBy { get; set; }
        public string? Status { get; set; }

        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public Department Department { get; set; }

    }
}
