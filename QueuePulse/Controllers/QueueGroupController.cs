using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using QueuePulse.DataAccess.Data;
using QueuePulse.Models;
using QueuePulse.Utility;

namespace QueuePulse.Controllers
{
	public class QueueGroupController : Controller
	{
		private readonly ApplicationDbContext _context;

		public QueueGroupController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: QueueGroup
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetGroups(string searchQuery = "", string statusFilter = "All")
		{
			var queueGroup = _context.QueueGroups.AsQueryable();

			if (!string.IsNullOrEmpty(searchQuery))
			{
				queueGroup = queueGroup.Where(d => d.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
												|| d.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));

			}

			if (statusFilter != "All")
			{
				//--This One Works--
				//queueGroup = queueGroup.Where(d => d.Status.ToUpper() == statusFilter.ToUpper());

				//--This One Doesnt Work--
				//queueGroup = queueGroup.Where(d =>
				//d.Status != null &&
				//d.Status.Trim().Equals(statusFilter.Trim(), StringComparison.OrdinalIgnoreCase));

				//--This One Works--
				queueGroup = queueGroup.Where(d =>
							d.Status != null &&
							EF.Functions.Like(d.Status.Trim(), statusFilter.Trim()));

			}

			return Json(await queueGroup.ToListAsync());
		}

		// GET: QueueGroup/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var queueGroup = await _context.QueueGroups
				.FirstOrDefaultAsync(m => m.Id == id);
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

				_context.Add(queueGroup);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(queueGroup);
		}

		// GET: QueueGroup/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var queueGroup = await _context.QueueGroups.FindAsync(id);
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy,Status")] QueueGroup queueGroup)
		{
			if (id != queueGroup.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(queueGroup);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!QueueGroupExists(queueGroup.Id))
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
			var queueGroup = await _context.QueueGroups.FindAsync(id);

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

				_context.QueueGroups.Update(queueGroup);
				await _context.SaveChangesAsync();

				return Json(new { success = true, message = "Service status updated successfully." });

			}

			return Json(new { success = false, message = "Service not found." });
		}

		// GET: QueueGroup/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var queueGroup = await _context.QueueGroups
				.FirstOrDefaultAsync(m => m.Id == id);
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
			var queueGroup = await _context.QueueGroups.FindAsync(id);
			if (queueGroup != null)
			{
				_context.QueueGroups.Remove(queueGroup);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool QueueGroupExists(int id)
		{
			return _context.QueueGroups.Any(e => e.Id == id);
		}
	}
}
