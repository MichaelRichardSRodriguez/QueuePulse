using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Repositories.RepoInterfaces;
using QueuePulse.DataAccess.Services.ServInterfaces;
using QueuePulse.Models;
using QueuePulse.Models.ViewModels;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{
    public class QueueServiceController : Controller
    {
        private readonly IServiceManagementService _serviceManagementService;

        public QueueServiceController(IServiceManagementService serviceManagementService)
        {
			_serviceManagementService = serviceManagementService;
        }

        // GET: QueueService
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.QueueServices.Include(q => q.Department);
            var queueServices = await _serviceManagementService.LoadServicesAsync(includeProperties: "Department");

            return View(queueServices.ToList());
        }

        // GET: QueueService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			//var queueService = await _context.QueueServices
			//    .Include(q => q.Department)
			//    .FirstOrDefaultAsync(m => m.Id == id);

			//var queueService = await _unitOfWork.QueueService.GetAllAsync(q => q.Id == id, includeProperties: "Department");
			var queueService  = await _serviceManagementService.LoadServicesAsync(q => q.Id == id, includeProperties: "Department");

			if (queueService == null)
            {
                return NotFound();
            }

            return View(queueService);
        }

        // GET: QueueService/Create
        public async Task<IActionResult> Create()
        {
            var servicesVM = await _serviceManagementService.GetServicesViewModelAsync();

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
                servicesVM.QueueService.CreatedDate = DateTime.Now;
                servicesVM.QueueService.CreatedBy = "MIKE";

                await _serviceManagementService.CreateNewServiceAsync(servicesVM.QueueService);

                return RedirectToAction(nameof(Index));
            }

            //servicesVM.DepartmentList = _context.Departments.Select(d => new SelectListItem
            //{
            //    Text = d.Name,
            //});
            //    Value = d.Id.ToString()

            return View(servicesVM);
        }

        // GET: QueueService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceVM = await _serviceManagementService.GetServicesViewModelAsync();

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
                    var servicesFromDb = await _serviceManagementService.GetServicesByIdAsync(id);

                    if (servicesFromDb == null)
                    {
                        return NotFound();
                    }

                    servicesFromDb.Name = servicesVM.QueueService.Name;
                    servicesFromDb.Description = servicesVM.QueueService.Description;
                    servicesFromDb.UpdatedDate = DateTime.Now;
                    servicesFromDb.UpdatedBy = "MIKE";

                    await _serviceManagementService.UpdateServiceAsync(servicesFromDb);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await QueueServiceExists(servicesVM.QueueService.Id))
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

            //servicesVM.DepartmentList = _context.Departments.Select(d => new SelectListItem
            //{
            //    Text = d.Name,
            //    Value = d.Id.ToString()
            //});

            return View(servicesVM);
        }

        // GET: QueueService/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueService = await _serviceManagementService.GetServicesByIdAsync(id);

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
            var queueService = await _serviceManagementService.GetServicesByIdAsync(id);
            if (queueService != null)
            {
                await _serviceManagementService.DeleteServiceAsyncAsync(queueService);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> QueueServiceExists(int id)
        {
            return await _serviceManagementService.isExistingServiceIdAsync(id);
        }
    }
}
