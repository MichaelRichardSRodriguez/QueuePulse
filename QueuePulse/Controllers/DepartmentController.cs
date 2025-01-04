using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.Data;
using QueuePulse.Models;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(string searchQuery = "", string statusFilter = "All")
        {
            var departments = _context.Departments.AsQueryable();

            if (statusFilter != "All")
            {
                //departments = departments.Where(d => d.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase));
                departments = departments.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                departments = departments.Where(d => d.Name.Contains(searchQuery) || d.Description.Contains(searchQuery));
            }

            return Json(await departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    department.CreatedDate = DateTime.Now;
                    department.CreatedBy = "MIKE";
                    department.Status = StaticDetails.ContentStatus_Active;
                    _context.Add(department);

                    await _context.SaveChangesAsync();

                    TempData["success"] = "New department successfully created.";

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                     TempData["error"] = $"Saving failed. Error Encountered.\n {ex.Message}";
                     //return BadRequest(ex);
                }

            }

            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					var existingDepartment = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == department.Id);

					if (existingDepartment == null)
					{
						return NotFound();
					}

					department.Status = existingDepartment.Status;
					department.CreatedDate = existingDepartment.CreatedDate;
					department.CreatedBy = existingDepartment.CreatedBy;
					department.UpdatedDate = DateTime.Now;
					department.UpdatedBy = "MIKE";

					_context.Update(department);

					TempData["success"] = "Department updated successfully.";

					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department != null)
            {
				TempData["success"] = "Department deleted successfully.";
				_context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //// POST: Department/Delete/5
        //[HttpPost, ActionName("UpdateStatus")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department != null)
            {

                if (department.Status == StaticDetails.ContentStatus_Active)
                {
                    department.Status = StaticDetails.ContentStatus_Inactive;
                }
                else
                {
                    department.Status = StaticDetails.ContentStatus_Active;
                }

                _context.Departments.Update(department);
                await _context.SaveChangesAsync();

                TempData["success"] = "Department status updated successfully.";

                return Json(new { success = true, message = "Department status updated successfully." });
            }

            return Json(new { success = false, message = "Department not found." });
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

    }
}
