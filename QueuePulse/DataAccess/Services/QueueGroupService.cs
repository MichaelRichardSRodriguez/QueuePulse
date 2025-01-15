using QueuePulse.DataAccess.UnitOfWork;
using QueuePulse.Models.Entities;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services
{
    public class QueueGroupService : IQueueGroupService
    {

        private readonly IUnitOfWork _unitOfWork;

        public QueueGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNewGroupAsync(QueueGroup queueGroup)
        {
            await _unitOfWork.QueueGroup.CreateAsync(queueGroup);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteGroupAsyncAsync(QueueGroup queueGroup)
        {
            _unitOfWork.QueueGroup.DeleteRecord(queueGroup);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateGroupAsync(QueueGroup queueGroup)
        {
            _unitOfWork.QueueGroup.UpdateGroup(queueGroup);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<QueueGroup>> GetAllGroupsAsync(Expression<Func<QueueGroup, bool>>? filter = null)
        {
            return await _unitOfWork.QueueGroup.GetAllAsync(filter);
        }

        public async Task<QueueGroup> GetGroupsByIdAsync(int id)
        {
            return await _unitOfWork.QueueGroup.GetByIdAsync(id);
        }

        public async Task<bool> isExistingGroupIdAsync(int id)
        {
            return await _unitOfWork.QueueGroup.ExistsAsync(g => g.Id == id);
        }

        public async Task<bool> isExistingGroupNameAsync(int id, string name)
        {
            return await _unitOfWork.QueueGroup.ExistsAsync(g => g.Id != id && g.Name == name);
        }

    }
}
