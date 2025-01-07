using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;

namespace QueuePulse.DataAccess.Repositories.ConcreteRepo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IDeparmentRepository Department { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Department = new DepartmentRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await   _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
