using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.Models;
using QueuePulse.Models.ViewModels;
using QueuePulse.Utility;
using QueuePulse.DataAccess.Services;

namespace QueuePulse.Controllers
{
    public class QueueServiceController : Controller
    {
        private readonly IServiceManagementService _service;

        public QueueServiceController(IServiceManagementService service)
        {
			_service = service;
        }

        // GET: QueueService
        public async Task<IActionResult> Index()
        {

            //var queueServices = await _service.GetAllServicesAsync();

            //return View(queueServices);

            return View();
		}

        [HttpGet]
        public async Task<IActionResult> GetServices(string searchQuery = "", string statusFilter = "All")
        {

            var queueServices = await _service.GetAllServicesAsync();

            if (statusFilter != "All")
            {
                queueServices = queueServices.Where(q => q.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                queueServices = queueServices.Where(q => q.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                                    || q.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

			return Json(queueServices);
        }

        // GET: QueueService/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var queueService = new ServicesVM()
            {
                QueueService = await _service.GetServicesByIdAsync(id),
                DepartmentList = await _service.GetDepartmentListAsync()
            };

			if (queueService == null)
            {
                return NotFound();
            }

            return View(queueService);
        }

        // GET: QueueService/Create
        public async Task<IActionResult> Create()
        {
            var servicesVM = new ServicesVM()
            {
                QueueService = new(),
                DepartmentList = await _service.GetDepartmentListAsync()
            };

            return View(servicesVM);
        }

        // POST: QueueService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicesVM servicesVM)
        {
            if (ModelState.IsValid)
            {

                if (!await _service.isExistingServiceNameAsync(servicesVM.QueueService.Id,servicesVM.QueueService.Name))
                {
                    servicesVM.QueueService.CreatedDate = DateTime.Now;
                    servicesVM.QueueService.CreatedBy = "MIKE";

                    await _service.CreateNewServiceAsync(servicesVM.QueueService);

                    return RedirectToAction(nameof(Index));

                }

                ModelState.AddModelError("QueueService.Name", "Service Name already exists.");

            }

            servicesVM.DepartmentList = await _service.GetDepartmentListAsync();

            return View(servicesVM);
        }

        // GET: QueueService/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

			var serviceVM = new ServicesVM()
			{
				QueueService = await _service.GetServicesByIdAsync(id),
				DepartmentList = await _service.GetDepartmentListAsync()
			};


			if (serviceVM.QueueService == null)
            {
                return NotFound();
            }

            return View(serviceVM);
        }

        // POST: QueueService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServicesVM servicesVM)
        {
            if (id != servicesVM.QueueService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var servicesFromDb = await _service.GetServicesByIdAsync(id);

                    if (servicesFromDb == null)
                    {
                        return NotFound();
                    }

                    servicesFromDb.Name = servicesVM.QueueService.Name;
                    servicesFromDb.Description = servicesVM.QueueService.Description;
                    servicesFromDb.Department_Id = servicesVM.QueueService.Department_Id;
                    servicesFromDb.UpdatedDate = DateTime.Now;
                    servicesFromDb.UpdatedBy = "MIKE";

                    await _service.UpdateServiceAsync(servicesFromDb);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.isExistingServiceIdAsync(id))
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

            servicesVM.DepartmentList = await _service.GetDepartmentListAsync();  

            return View(servicesVM);
        }

        // GET: QueueService/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueService = await _service.GetServicesByIdAsync(id);

            if (queueService == null)
            {
                return NotFound();
            }

            return View(queueService);
        }

        // POST: QueueService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queueService = await _service.GetServicesByIdAsync(id);
            if (queueService != null)
            {
                await _service.DeleteServiceAsyncAsync(queueService);
            }

            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> UpdateStatus(int id)
		{
			var department = await _service.GetServicesByIdAsync(id);
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

				await _service.UpdateServiceAsync(department);

				return Json(new { success = true, message = "Service status updated successfully." });

			}

			return Json(new { success = false, message = "Service not found." });
		}
	}
}
