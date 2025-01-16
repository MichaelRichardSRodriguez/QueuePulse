using QueuePulse.DataAccess.UnitOfWork;
using QueuePulse.Models.DTO;
using QueuePulse.Models.Entities;
using System.Linq.Expressions;

namespace QueuePulse.DataAccess.Services
{
    public class DepartmentManagementService : IDepartmentManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNewDepartmentAsync(DepartmentDTO departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedDate = departmentDto.CreatedDate,
                CreatedBy = departmentDto.CreatedBy
            };

            await _unitOfWork.Department.CreateAsync(department);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteDepartment(DepartmentDTO departmentDto)
        {
            Department department = await _unitOfWork.Department.GetByIdAsync(departmentDto.Id);

            if (department != null)
            {
                _unitOfWork.Department.DeleteRecord(department);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> isExistingDepartmentId(int id)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id == id);
        }

        public async Task<bool> isExistingDepartmentName(int id, string name)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id != id && d.Name == name);
        }

        public async Task<bool> isExistingDepartmentName(int id)
        {
            return await _unitOfWork.Department.ExistsAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DepartmentDTO>> LoadDepartmentsAsync()
        {
            var department = await _unitOfWork.Department.GetAllAsync();

            return department.Select(department => new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Status = department.Status,
                CreatedDate = department.CreatedDate,
                UpdatedDate = department.UpdatedDate,
                CreatedBy = department.CreatedBy,
                UpdatedBy = department.UpdatedBy
            }).ToList();

            // return await _unitOfWork.Department.GetAllAsync();

        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(id);

            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Status = department.Status,
                CreatedDate = department.CreatedDate,
                UpdatedDate = department.UpdatedDate,
                CreatedBy = department.CreatedBy,
                UpdatedBy = department.UpdatedBy
            };

            //return department != null ? new DepartmentDTO
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Description = department.Description,
            //    Status = department.Status,
            //    CreatedDate = department.CreatedDate,
            //    UpdatedDate = department.UpdatedDate,
            //    CreatedBy = department.CreatedBy,
            //    UpdatedBy = department.UpdatedBy
            //} : null;

            return departmentDto;

            //return await _unitOfWork.Department.GetByIdAsync(id);
        }

        public async Task UpdateDepartmentAsync(DepartmentDTO departmentDto)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(departmentDto.Id);

            if (department != null)
            {

                department.Name = departmentDto.Name;
                department.Description = departmentDto.Description;
                department.Status = departmentDto.Status;
                department.UpdatedBy = departmentDto.UpdatedBy;
                department.UpdatedDate = departmentDto.UpdatedDate;

                _unitOfWork.Department.UpdateDepartment(department);
                await _unitOfWork.CompleteAsync();
            }
        }

    }
}
