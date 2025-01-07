namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IDeparmentRepository Department { get; }

        Task CompleteAsync();

    }
}
