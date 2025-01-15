using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{
    public class QueueGroupController : Controller
	{
		private readonly IQueueGroupService _service;


		public QueueGroupController(IQueueGroupService service)
		{
			_service = service;
		}

		// GET: QueueGroup
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetGroups(string searchQuery = "", string statusFilter = "All")
		{
			var queueGroup = await _service.GetAllGroupsAsync();

            if (statusFilter != "All")
            {
                ////-- This One Works when using _context --
                //queueGroup = queueGroup.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());

				//--This One Doesnt Work--
				queueGroup = queueGroup.Where(d =>
				d.Status != null &&
				d.Status.Trim().Equals(statusFilter.Trim(), StringComparison.OrdinalIgnoreCase));

				////--This One Works--
				//queueGroup = queueGroup.Where(d =>
				//            d.Status != null &&
				//            EF.Functions.Like(d.Status.Trim(), statusFilter.Trim()));

			}

            if (!string.IsNullOrEmpty(searchQuery))
			{
                queueGroup = queueGroup.Where(d => d.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                								|| d.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

			return Json(queueGroup.ToList());
		}

		// GET: QueueGroup/Details/5
		public async Task<IActionResult> Details(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var queueGroup = await _service.GetGroupsByIdAsync(id);

            if (queueGroup == null)
			{
				return NotFound();
			}

			return View(queueGroup);
		}

		// GET: QueueGroup/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: QueueGroup/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Description")] QueueGroup queueGroup)
		{
			if (ModelState.IsValid)
			{
				queueGroup.CreatedDate = DateTime.Now;
				queueGroup.CreatedBy = "MIKE";

				await _service.CreateNewGroupAsync(queueGroup);

				return RedirectToAction(nameof(Index));
			}
			return View(queueGroup);
		}

		// GET: QueueGroup/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var queueGroup = await _service.GetGroupsByIdAsync(id);

            if (queueGroup == null)
			{
				return NotFound();
			}
			return View(queueGroup);
		}

		// POST: QueueGroup/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] QueueGroup queueGroup)
		{
			if (id != queueGroup.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var groupFromDb = await _service.GetGroupsByIdAsync(id);
					if (groupFromDb == null)
					{
						return NotFound();
					
					}

                    groupFromDb.Name = queueGroup.Name;
					groupFromDb.Description = queueGroup.Description;
					groupFromDb.UpdatedDate = DateTime.Now;
					groupFromDb.UpdatedBy = "MIKE";

					await _service.UpdateGroupAsync(groupFromDb);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _service.isExistingGroupIdAsync(queueGroup.Id))
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
			return View(queueGroup);
		}

		public async Task<IActionResult> UpdateStatus(int id)
		{
			var queueGroup = await _service.GetGroupsByIdAsync(id);

			if (queueGroup != null)
			{

				if (queueGroup.Status == StaticDetails.ContentStatus_Active)
				{
					queueGroup.Status = StaticDetails.ContentStatus_Inactive;
				}
				else
				{
					queueGroup.Status = StaticDetails.ContentStatus_Active;
				}

				await _service.UpdateGroupAsync(queueGroup);

				return Json(new { success = true, message = "Service status updated successfully." });

			}

			return Json(new { success = false, message = "Service not found." });
		}

		// GET: QueueGroup/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var queueGroup = await _service.GetGroupsByIdAsync(id);

			if (queueGroup == null)
			{
				return NotFound();
			}

			return View(queueGroup);
		}

		// POST: QueueGroup/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var queueGroup = await  _service.GetGroupsByIdAsync(id);
			if (queueGroup != null)
			{
				await _service.DeleteGroupAsyncAsync(queueGroup);
			}

			return RedirectToAction(nameof(Index));
		}

	}
}
