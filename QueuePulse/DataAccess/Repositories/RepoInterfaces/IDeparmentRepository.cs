using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IDeparmentRepository: IRepository<Department>
    {

        void UpdateDepartment(Department department);

    }
}
