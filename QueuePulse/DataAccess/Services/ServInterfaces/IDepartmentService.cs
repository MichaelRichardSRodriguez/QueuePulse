using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Services.ServInterfaces
{
    public interface IDepartmentService
    {

        Task<IEnumerable<Department>> LoadDepartmentsAsync();
        Task<bool> isExistingDepartmentName(int id, string name);
        Task<bool> isExistingDepartmentId(int id);
        Task CreateNewDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartment(Department department);

    }
}
