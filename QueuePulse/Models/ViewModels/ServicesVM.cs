using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using QueuePulse.Models.Entities;

namespace QueuePulse.Models.ViewModels
{
    public class ServicesVM
    {
		public QueueService QueueService { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> DepartmentList { get; set; }

	}
}
