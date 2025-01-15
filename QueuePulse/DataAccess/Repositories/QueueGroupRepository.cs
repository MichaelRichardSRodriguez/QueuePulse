using QueuePulse.DataAccess.Data;
using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Repositories
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
            _context.QueueGroups.Update(queueGroup);
        }
    }
}
