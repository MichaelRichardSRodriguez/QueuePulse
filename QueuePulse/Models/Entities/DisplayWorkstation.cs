using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueuePulse.Models.Entities
{
	public class DisplayWorkstation
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public string? CurrentQueue { get; set; }
		public string Status { get; set; }

		[ForeignKey("ProfileId")]
		[ValidateNever]
		public Profile Profile { get; set; }

	}
}
