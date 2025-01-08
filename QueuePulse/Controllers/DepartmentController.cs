using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services.ServInterfaces;
using QueuePulse.Models;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service) //ApplicationDbContext context)
        {
            //_context = context;
            _service = service;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(string searchQuery = "", string statusFilter = "All")
        {
            var departments = await _service.LoadDepartmentsAsync();

            if (statusFilter != "All")
            {
                departments = departments.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                departments = departments.Where(d => d.Name.Contains(searchQuery,StringComparison.OrdinalIgnoreCase) || d.Description.ToUpper().Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return Json(departments.ToList());
        }


        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _service.GetDepartmentByIdAsync(id); 
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

                    await _service.CreateNewDepartmentAsync(department); 

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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _service.GetDepartmentByIdAsync(id); 
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
        public async Task<IActionResult> Edit(int id,  Department newDepartment) //[Bind("Id,Name,Description")] 
        {
            if (id != newDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var departmentFromDb = await _service.GetDepartmentByIdAsync(id);  

					if (departmentFromDb == null)
					{
						return NotFound();
					}

                    departmentFromDb.Name = newDepartment.Name;
                    departmentFromDb.Description = newDepartment.Description;
                    departmentFromDb.UpdatedDate = DateTime.Now;
                    departmentFromDb.UpdatedBy = "MIKE";

                    await _service.UpdateDepartmentAsync(departmentFromDb); 

					TempData["success"] = "Department updated successfully.";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(newDepartment.Id))
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
            return View(newDepartment);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _service.GetDepartmentByIdAsync(id); 

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
            var department = await _service.GetDepartmentByIdAsync(id);

            if (department != null)
            {
				TempData["success"] = "Department deleted successfully.";
                await _service.DeleteDepartment(department);
            }

            return RedirectToAction(nameof(Index));
        }

        //// POST: Department/Delete/5
        //[HttpPost, ActionName("UpdateStatus")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string searchQuery = "", string statusFilter = "All")
        {
            var department = await _service.GetDepartmentByIdAsync(id);
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

                await _service.UpdateDepartmentAsync(department);

                //TempData["success"] = "Department status updated successfully.";

                return Json(new { success = true, message = "Department status updated successfully." });
                //return Json(department);

            }

            return Json(new { success = false, message = "Department not found." });
        }

        private async Task<bool> DepartmentExists(int id)
        {

            return await _service.isExistingDepartmentId(id);
        }

    }
}
