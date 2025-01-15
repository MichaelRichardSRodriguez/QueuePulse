using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.Models.Entities;

namespace QueuePulse.DataAccess.Repositories
{
    public class QueueServiceRepository : Repository<QueueService>, IQueueServiceRepository
    {
        private ApplicationDbContext _context;

        public QueueServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void UpdateQueueService(QueueService queueService)
        {
            _context.QueueServices.Update(queueService);
        }

    }
}
