using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Repositories
{
    public interface IQueueGroupRepository : IRepository<QueueGroup>
    {
        void UpdateGroup(QueueGroup queueGroup);

    }
}
