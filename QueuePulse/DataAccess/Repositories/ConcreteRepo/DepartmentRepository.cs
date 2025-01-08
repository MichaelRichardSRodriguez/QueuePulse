using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.ConcreteRepo
{
    public class DepartmentRepository : Repository<Department>, IDeparmentRepository
    {
        private ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void UpdateDepartment(Department department)
        {
            _context.Update(department);
            
        }
    }
}
