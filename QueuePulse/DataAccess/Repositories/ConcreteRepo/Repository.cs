using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Repositories.ConcreteRepo
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void DeleteRecord(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            //var queryObject = _dbSet.AsQueryable();
            IQueryable<T> queryObject = _dbSet;

			if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    queryObject =  queryObject.Include(property);
                }

            }

			if (filter != null)
			{
				queryObject = queryObject.Where(filter);
			}

			return  await queryObject.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, string? includeProperties = null)
        {
            var queryObject = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryObject = queryObject.Include(property);
                }
            }

            return await queryObject.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        }
    }
}
