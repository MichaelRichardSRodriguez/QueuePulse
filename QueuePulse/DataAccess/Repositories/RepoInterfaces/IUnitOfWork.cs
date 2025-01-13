namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IDepartmentRepository Department { get; }
        IQueueServiceRepository QueueService { get; }
        IQueueGroupRepository QueueGroup { get; }

        Task CompleteAsync();

    }
}
