using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.ConcreteRepo
{
    public class QueueGroupRepository : Repository<QueueGroup>, IQueueGroupRepository
    {

        private readonly ApplicationDbContext _context;
        public QueueGroupRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateGroup(QueueGroup queueGroup)
        {
            throw new NotImplementedException();
        }
    }
}
