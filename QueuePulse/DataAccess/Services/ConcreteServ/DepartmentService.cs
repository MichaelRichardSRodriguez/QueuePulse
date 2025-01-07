using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.DataAccess.Services.ServInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Services.ConcreteServ
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNewDepartmentAsync(Department department)
        {
            await _unitOfWork.Department.CreateAsync(department);
            await _unitOfWork.CompleteAsync();
        }

        public void DeleteDepartment(Department department)
        {
            _unitOfWork.Department.DeleteRecord(department);
        }

        public async Task<bool> isExistingDepartmentId(int id)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id == id);
        }

        public async Task<bool> isExistingDepartmentName(int id, string name)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id == id && d.Name == name);
        }

        public async Task<bool> isExistingDepartmentName(int id)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> LoadDepartmentsAsync()
        {
            return await _unitOfWork.Department.GetAllAsync();
        }

        public async Task<Department> ShowDepartmentDetailsAsync(int id)
        {
            return await _unitOfWork.Department.GetByIdAsync(id);
        }

        public void UpdateDepartment(Department department)
        {
            _unitOfWork.Department.UpdateDepartment(department);
        }
    }
}
