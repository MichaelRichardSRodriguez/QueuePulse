
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Services;
using QueuePulse.Models.DTO;
using QueuePulse.Models.Entities;
using QueuePulse.Models.ViewModels;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{

    public class DepartmentController : Controller
    {

        private readonly IDepartmentManagementService _service;

        public DepartmentController(IDepartmentManagementService service) //ApplicationDbContext context)
        {
            _service = service;
        }

        // GET: Department
        public IActionResult Index()
        {

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(string searchQuery = "", string statusFilter = "All", int recordPerPage = 10)// ,int pageNo = 1)
        {
            var departments = await _service.LoadDepartmentsAsync();

            if (statusFilter != "All")
            {
                departments = departments.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                departments = departments.Where(d => d.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || d.Description.ToUpper().Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            //For Pagination Codes
            var recordCount = departments.Count();  //Record Count
            var totalPages = (int)Math.Ceiling(recordCount / (double)recordPerPage); //Get Number of Pages

            //departments = departments.Take(recordPerPage);

            ViewData["DepartmentPages"] = totalPages;
            ViewData["DepartmentCount"] = recordCount;

            return Json(departments.ToList());


            //         //*********************************************

            //         DepartmentPaginatedVM departments = new()
            //         {
            //             Department = await _service.LoadDepartmentsAsync(),
            //             CurrentPage = pageNo,
            //             TotalPages = 0
            //};


            //if (statusFilter != "All")
            //{
            //	departments.Department = departments.Department.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());
            //}

            //if (!string.IsNullOrEmpty(searchQuery))
            //{
            //	departments.Department = departments.Department.Where(d => d.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || d.Description.ToUpper().Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            //}

            ////For Pagination Codes
            //var recordCount = departments.Department.Count();  //Record Count
            //var totalPages = (int)Math.Ceiling(recordCount / (double)recordPerPage); //Get Number of Pages

            //departments.Department = departments.Department.Take(recordPerPage);
            //         departments.TotalPages = totalPages;
            //         departments.CurrentPage = pageNo;


            //return Json(departments);

        }


        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            DepartmentDTO departmentDTO = await _service.GetDepartmentByIdAsync(id);

            var department = new Department
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                Status = departmentDTO.Status,
                CreatedDate = departmentDTO.CreatedDate,
                CreatedBy = departmentDTO.CreatedBy,
                UpdatedDate = departmentDTO.UpdatedDate,
                UpdatedBy = departmentDTO.UpdatedBy
            };

            if (departmentDTO == null)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] DepartmentDTO departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _service.isExistingDepartmentName(departmentDto.Id, departmentDto.Name))
                    {
                        departmentDto.CreatedDate = DateTime.Now;
                        departmentDto.CreatedBy = "MIKE";

                        await _service.CreateNewDepartmentAsync(departmentDto);

                        TempData["success"] = "New department successfully created.";

                        return RedirectToAction(nameof(Index));

                    }

                    ModelState.AddModelError("Name", "Department Name already exists.");

                }
                catch (Exception ex)
                {

                    TempData["error"] = $"Saving failed. Error Encountered.\n {ex.Message}";
                    //return BadRequest(ex);
                }

            }

            return View(departmentDto);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var departmentDto = await _service.GetDepartmentByIdAsync(id); 
            if (departmentDto == null)
            {
                return NotFound();
            }

            var department = new Department
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Department newDepartment) //[Bind("Id,Name,Description")] 
        {
            //       if (id != newDepartment.Id)
            //       {
            //           return NotFound();
            //       }

            //       if (ModelState.IsValid)
            //       {
            //           try
            //           {

            //               if (await _service.isExistingDepartmentName(newDepartment.Id, newDepartment.Name))
            //               {
            //                   ModelState.AddModelError("Name", "Department Name already exists.");

            //                   return View(newDepartment);
            //               }

            //               var departmentFromDb = await _service.GetDepartmentByIdAsync(id);  

            //if (departmentFromDb == null)
            //{
            //	return NotFound();
            //}

            //               departmentFromDb.Name = newDepartment.Name;
            //               departmentFromDb.Description = newDepartment.Description;
            //               departmentFromDb.UpdatedDate = DateTime.Now;
            //               departmentFromDb.UpdatedBy = "MIKE";

            //               await _service.UpdateDepartmentAsync(departmentFromDb); 

            //TempData["success"] = "Department updated successfully.";

            //           }
            //           catch (DbUpdateConcurrencyException)
            //           {
            //               if (!await _service.isExistingDepartmentId(id))
            //               {
            //                   return NotFound();
            //               }
            //               else
            //               {
            //                   throw;
            //               }
            //           }

            //           return RedirectToAction(nameof(Index));
            //       }

            //       return View(newDepartment);

            //-------------- REVISED Using DTO ----------------------

            if (id != newDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (await _service.isExistingDepartmentName(newDepartment.Id, newDepartment.Name))
                    {
                        ModelState.AddModelError("Name", "Department Name already exists.");

                        return View(newDepartment);
                    }

                    var departmentDto = await _service.GetDepartmentByIdAsync(id);

                    if (departmentDto == null)
                    {
                        return NotFound();
                    }

                    departmentDto.Name = newDepartment.Name;
                    departmentDto.Description = newDepartment.Description;
                    departmentDto.UpdatedDate = DateTime.Now;
                    departmentDto.UpdatedBy = "MIKE";

                    await _service.UpdateDepartmentAsync(departmentDto);

                    TempData["success"] = "Department updated successfully.";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.isExistingDepartmentId(id))
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
            if (id == 0)
            {
                return NotFound();
            }

            var departmentDto = await _service.GetDepartmentByIdAsync(id);

            var department = new Department
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                Status = departmentDto.Status,
                CreatedBy = departmentDto.CreatedBy,
                CreatedDate = departmentDto.CreatedDate,
                UpdatedBy = departmentDto.UpdatedBy,
                UpdatedDate = departmentDto.UpdatedDate
            };

            if (departmentDto == null)
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
            var departmentDto = await _service.GetDepartmentByIdAsync(id);

            if (departmentDto != null)
            {
                try
                {
                    await _service.DeleteDepartment(departmentDto);
                    TempData["success"] = "Department deleted successfully.";
                }
                catch (DbUpdateException)
                {
                    TempData["error"] = "Record Deletion Unsuccessful.";
                    return BadRequest("Unable to Delete Department, because it has existing services.\n"
                                        + "You can DEACTIVATE this Department to prevent users from using it \n"
                                        + "or you can delete the Services first before deleting this department.");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        //// POST: Department/Delete/5
        //[HttpPost, ActionName("UpdateStatus")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);
            if (department != null)
            {

                if (department.Status == StaticDetails.CONTENTSTATUS_ACTIVE)
                {
                    department.Status = StaticDetails.CONTENTSTATUS_INACTIVE;
                }
                else
                {
                    department.Status = StaticDetails.CONTENTSTATUS_ACTIVE;
                }

                await _service.UpdateDepartmentAsync(department);

                //TempData["success"] = "Department status updated successfully.";

                return Json(new { success = true, message = "Department status updated successfully." });
                //return Json(department);

            }

            return Json(new { success = false, message = "Department not found." });
        }

    }
}
