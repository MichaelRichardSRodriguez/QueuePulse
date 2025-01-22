using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services;
using QueuePulse.Models.Entities;
using QueuePulse.Utility;
using Queuing_System.Hubs;

namespace QueuePulse.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]

    public class QueueController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IHubContext<QueueHub> _hubContext;
		private readonly TextToSpeechService _textToSpeechService;

		public QueueController(ApplicationDbContext context, IHubContext<QueueHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
			_textToSpeechService = new TextToSpeechService();
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
                await _hubContext.Clients.All.SendAsync("ReceiveQueueUpdate", false, "", "");

            }

            //return View(queueItem);
            return RedirectToAction(nameof(Ticket));
        }

        public async Task<IActionResult> ManageQueue()
		{

			var ticketsVM = await _context.Tickets.ToListAsync();

			return View(ticketsVM);
		}

        public async Task<IActionResult> QueueDisplay()
        {
			var workstations = await _context.Profiles
					   .Where(profiles => !_context.DisplayWorkstations.AsNoTracking()
					   .Any(dw => dw.ProfileId == profiles.Id))
					   .ToListAsync();

			if (workstations != null)
			{
				var newDisplayWorkstation = workstations.Select(profile => new DisplayWorkstation
				{
					ProfileId = profile.Id,
					Status = StaticDetails.DISPLAY_ONLINE,
					CurrentQueue = "...."
				}).ToList();

				await _context.DisplayWorkstations.AddRangeAsync(newDisplayWorkstation);
				await _context.SaveChangesAsync();

			}

			var queueItems = await _context.DisplayWorkstations
				.Include(p => p.Profile)
				.Where(w => w.Status == StaticDetails.DISPLAY_ONLINE)
				.OrderBy(p => p.Profile.Department.Name)
				.ThenBy(p => p.Profile.Name)
				.ToListAsync();

			return View(queueItems);
		}

		public async Task<IActionResult> GetCurrentQueueLists()
		{
			var queueItems = await _context.DisplayWorkstations
				.Include(p => p.Profile)
				.Where(w => w.Status == StaticDetails.DISPLAY_ONLINE)
				.OrderBy(p => p.Profile.Department.Name)
				.ThenBy(p => p.Profile.Name)
				.ToListAsync();


			//var queueItems = await _context.QueueItems
			//				.Include(c => c.Category)
			//				.Where(q => q.Status == StaticDetails.Status_Processing)
			//				.GroupBy(q => q.ServiceId)
			//				.Select(q => q.OrderByDescending(q => q.DateCreated).FirstOrDefault())
			//				.ToListAsync();

			return Json(queueItems);

		}

		// This will handle the form submission
		[HttpPost]
		public async Task<ActionResult> SpeakText(string ticketNo, string counterName)
		{
			if (!string.IsNullOrEmpty(ticketNo))
			{
				await _textToSpeechService.ConvertTextToSpeech($"Now calling {ticketNo}, you may now approach the counter {counterName}");
			}

			//return RedirectToAction(nameof(Index));
			return Json(new {success = true});
		}
	}
}
