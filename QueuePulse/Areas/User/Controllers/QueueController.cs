using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;

namespace QueuePulse.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]

    public class QueueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QueueController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ticket =  _context.Tickets.ToList();
            return View(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> Ticket()
        {

            var servicesVM = await _context.QueueServices.ToListAsync();

            return View(servicesVM);
        }

        // POST: /Queue/Enqueue
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQueue(int serviceId, Ticket newTicket)
        {
            if (ModelState.IsValid)
            {

                //Temporary Code
                var queue = await _context.Tickets.AsNoTracking()
                            .Where(q => q.ServiceId == serviceId)
                            .CountAsync() + 1;
                var prefix = await _context.QueueServices.AsNoTracking()
                            .Where(q => q.Id == serviceId).Select(s => s.Prefix)
                            .FirstOrDefaultAsync();

                newTicket.TicketNo = prefix + queue.ToString("000");

                newTicket.Status = StaticDetails.QUEUE_NEW;
                newTicket.ServiceId = serviceId;
                newTicket.CreatedDate = DateTime.Now;

                _context.Tickets.Add(newTicket);

                await _context.SaveChangesAsync();

                // Notify clients about the new queue item
                //await _hubContext.Clients.All.SendAsync("ReceiveQueueUpdate", false, "", "");

            }

            //return View(queueItem);
            return RedirectToAction(nameof(Ticket));
        }

		public async Task<IActionResult> ManageQueue()
		{

			var ticketsVM = await _context.Tickets.ToListAsync();

			return View(ticketsVM);
		}
	}
}
