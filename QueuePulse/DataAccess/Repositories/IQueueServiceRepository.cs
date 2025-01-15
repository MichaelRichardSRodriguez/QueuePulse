using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Repositories
{
    public interface IQueueServiceRepository : IRepository<QueueService>
    {
        void UpdateQueueService(QueueService queueService);
    }
}
