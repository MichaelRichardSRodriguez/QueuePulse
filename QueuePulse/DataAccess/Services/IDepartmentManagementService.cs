using QueuePulse.Models.DTO;
using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Services
{
    public interface IDepartmentManagementService
    {

        Task<IEnumerable<DepartmentDTO>> LoadDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task CreateNewDepartmentAsync(DepartmentDTO department);
        Task UpdateDepartmentAsync(DepartmentDTO department);
        Task DeleteDepartment(DepartmentDTO department);
        Task<bool> isExistingDepartmentName(int id, string name);
        Task<bool> isExistingDepartmentId(int id);

    }
}
