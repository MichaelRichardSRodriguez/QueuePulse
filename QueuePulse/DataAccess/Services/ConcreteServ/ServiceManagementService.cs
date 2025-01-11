using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.DataAccess.Services.ServInterfaces;
using QueuePulse.Models;
using QueuePulse.Models.ViewModels;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services.ConcreteServ
{
	public class ServiceManagementService : IServiceManagementService
	{
		private readonly IUnitOfWork _unitOfWork;

        public ServiceManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task CreateNewServiceAsync(QueueService queueService)
		{
			await _unitOfWork.QueueService.CreateAsync(queueService);
			await _unitOfWork.CompleteAsync();
		}


		public async Task DeleteServiceAsyncAsync(QueueService queueService)
		{
			_unitOfWork.QueueService.DeleteRecord(queueService);
			await _unitOfWork.CompleteAsync();

		}

		public async Task<QueueService> GetServicesByIdAsync(int id)
		{
			return await _unitOfWork.QueueService.GetByIdAsync(id);
		}

		public async Task<ServicesVM> GetServicesViewModelAsync()
		{
			var servicesVM = new ServicesVM()
			{
				QueueService = new QueueService(),
				DepartmentList = await _unitOfWork.Department.GetDepartmentListAsync()
			};

			return servicesVM;
		}

		public async Task<bool> isExistingServiceIdAsync(int id)
		{
			return await _unitOfWork.QueueService.ExistsAsync(q => q.Id == id);
				
		}

		public async Task<bool> isExistingServiceNameAsync(int id, string name)
		{
			return await _unitOfWork.QueueService.ExistsAsync(q => q.Id != id && q.Name == name);
		}

		public async Task<IEnumerable<QueueService>> LoadServicesAsync(Expression<Func<QueueService, bool>>? filter = null, string? includeProperties = null)
		{

			return await _unitOfWork.QueueService.GetAllAsync(filter,includeProperties);
		}

		public async Task UpdateServiceAsync(QueueService queueService)
		{
			_unitOfWork.QueueService.UpdateQueueService(queueService);
			await _unitOfWork.CompleteAsync();
		}
	}
}
