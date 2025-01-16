using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {

        Task<IEnumerable<SelectListItem>> GetDepartmentListAsync();
        void UpdateDepartment(Department departmentDto);

    }
}
