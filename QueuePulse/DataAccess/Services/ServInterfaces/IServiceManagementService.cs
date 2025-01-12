using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.Models;
using QueuePulse.Models.ViewModels;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services.ServInterfaces
{
	public interface IServiceManagementService
	{
		Task<IEnumerable<QueueService>> GetAllServicesAsync(Expression<Func<QueueService, bool>>? filter = null);
		Task<QueueService> GetServicesByIdAsync(int id);
		Task<bool> isExistingServiceNameAsync(int id, string name);
		Task<bool> isExistingServiceIdAsync(int id);
		Task CreateNewServiceAsync(QueueService queueService);
		Task DeleteServiceAsyncAsync(QueueService queueService);
		Task UpdateServiceAsync(QueueService queueService);
		Task<IEnumerable<SelectListItem>> GetDepartmentListAsync();

	}
}
