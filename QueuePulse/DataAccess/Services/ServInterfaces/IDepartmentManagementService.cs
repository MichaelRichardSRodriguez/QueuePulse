using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Services.ServInterfaces
{
    public interface IDepartmentManagementService
    {

        Task<IEnumerable<Department>> LoadDepartmentsAsync();
		Task<Department> GetDepartmentByIdAsync(int id);
        Task CreateNewDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartment(Department department);
		Task<bool> isExistingDepartmentName(int id, string name);
		Task<bool> isExistingDepartmentId(int id);

	}
}
