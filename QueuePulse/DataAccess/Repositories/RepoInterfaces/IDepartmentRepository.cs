using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IDepartmentRepository: IRepository<Department>
    {

		Task<IEnumerable<SelectListItem>> GetDepartmentListAsync();
		void UpdateDepartment(Department department);

    }
}
