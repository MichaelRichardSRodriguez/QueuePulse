using Microsoft.AspNetCore.Mvc.Rendering;
using QueuePulse.Models;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services.ServInterfaces
{
    public interface IQueueGroupService
    {
        Task<IEnumerable<QueueGroup>> GetAllGroupsAsync(Expression<Func<QueueGroup, bool>>? filter = null);
        Task<QueueGroup> GetGroupsByIdAsync(int id);
        Task<bool> isExistingGroupNameAsync(int id, string name);
        Task<bool> isExistingGroupIdAsync(int id);
        Task CreateNewGroupAsync(QueueGroup queueGroup);
        Task DeleteGroupAsyncAsync(QueueGroup queueGroup);
        Task UpdateGroupAsync(QueueGroup queueGroup);

    }
}
