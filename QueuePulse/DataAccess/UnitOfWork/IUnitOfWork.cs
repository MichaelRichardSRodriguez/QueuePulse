using QueuePulse.DataAccess.Repositories;

namespace QueuePulse.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Department { get; }
        IQueueServiceRepository QueueService { get; }
        IQueueGroupRepository QueueGroup { get; }

        Task CompleteAsync();

    }
}
