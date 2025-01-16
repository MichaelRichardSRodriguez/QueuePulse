using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QueuePulse.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QueuePulse.Models.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Status { get; set; }

        [ValidateNever]
        public IEnumerable<QueueService> Services { get; set; }

    }
}
