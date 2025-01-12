using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
    public interface IRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter=null, string? includeProperties=null);
        Task<T> GetByIdAsync(int id, string? includeProperties = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        void DeleteRecord(T entity);
        
    }


}
