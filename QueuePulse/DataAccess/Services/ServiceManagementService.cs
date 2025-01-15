using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.DataAccess.UnitOfWork;
using QueuePulse.Models.Entities;
using QueuePulse.Models.ViewModels;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services
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
            return await _unitOfWork.QueueService.GetByIdAsync(id, "Department");
        }

        public async Task<IEnumerable<SelectListItem>> GetDepartmentListAsync()
        {
            return await _unitOfWork.Department.GetDepartmentListAsync();

        }

        public async Task<bool> isExistingServiceIdAsync(int id)
        {
            return await _unitOfWork.QueueService.ExistsAsync(q => q.Id == id);

        }

        public async Task<bool> isExistingServiceNameAsync(int id, string name)
        {
            return await _unitOfWork.QueueService.ExistsAsync(q => q.Id != id && q.Name == name);
        }

        public async Task<IEnumerable<QueueService>> GetAllServicesAsync(Expression<Func<QueueService, bool>>? filter = null)
        {
            return await _unitOfWork.QueueService.GetAllAsync(includeProperties: "Department");

        }

        public async Task UpdateServiceAsync(QueueService queueService)
        {
            _unitOfWork.QueueService.UpdateQueueService(queueService);
            await _unitOfWork.CompleteAsync();
        }
    }
}
