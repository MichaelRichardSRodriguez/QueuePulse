using QueuePulse.Models.Entities;

namespace QueuePulse.Models.ViewModels
{
    public class DepartmentPaginatedVM
	{
		public IEnumerable<Department> Department { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }

	}
}
