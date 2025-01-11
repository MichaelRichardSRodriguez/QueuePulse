using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.Models;

namespace QueuePulse.DataAccess.Repositories.ConcreteRepo
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
		public async Task<IEnumerable<SelectListItem>> GetDepartmentListAsync()
		{
			return await _context.Departments.Select(d => new SelectListItem
			{
				Text = d.Name,
				Value = d.Id.ToString()
			}).ToListAsync();
		}

		public void UpdateDepartment(Department obj)
        {
            _context.Departments.Update(obj);
            
        }
    }
}
