using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IQueueGroupRepository: IRepository<QueueGroup>
    {
        void UpdateGroup(QueueGroup queueGroup);

    }
}
