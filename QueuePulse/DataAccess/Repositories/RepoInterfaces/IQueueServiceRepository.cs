using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.DataAccess.Repositories.ConcreteRepo;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.RepoInterfaces
{
	public interface IQueueServiceRepository: IRepository<QueueService>
	{
		void UpdateQueueService(QueueService queueService);
	}
}
