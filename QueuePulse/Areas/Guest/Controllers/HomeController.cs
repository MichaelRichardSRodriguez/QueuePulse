using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services;
using QueuePulse.Models;
using QueuePulse.Models.Entities;
using QueuePulse.Models.ViewModels;
using QueuePulse.Utility;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace QueuePulse.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly TextToSpeechService _textToSpeechService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _textToSpeechService = new TextToSpeechService();
        }

        public IActionResult Index()
        {
            bool isConnectionSuccessful = false;

            try
            {
                isConnectionSuccessful = _context.Database.CanConnect();
            }
            catch (Exception)
            {
               
                throw;
            }

            ViewData["ConnectionStatus"] = isConnectionSuccessful? "Connection established successfully!": "Connection Failed.";

            return View();
        }

        // This will handle the form submission
        [HttpPost]
        public async Task<ActionResult> SpeakText(string ticketNo, string counterName)
        {
            if (!string.IsNullOrEmpty(ticketNo))
            {
                await   _textToSpeechService.ConvertTextToSpeech($"Now calling {ticketNo}, you may now approach the counter {counterName}");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {
			// Fetch Departments and Queue Services
			var departments = await _context.Departments.ToListAsync();
			var queueServices = await  _context.QueueServices.Include(q => q.Department).ToListAsync();

			// Fetch Ticket Statuses
			var ticketStats = new TicketStat
			{
				New = await _context.Tickets.CountAsync(t => t.Status == StaticDetails.QUEUE_NEW),
				InProgress = await _context.Tickets.CountAsync(t => t.Status == StaticDetails.QUEUE_INPROGRESS),
				Completed = await _context.Tickets.CountAsync(t => t.Status == StaticDetails.QUEUE_COMPLETED)
			};

			Console.WriteLine($"New: {ticketStats.New}, InProgress: {ticketStats.InProgress}, Completed: {ticketStats.Completed}");


			// Create the ViewModel and pass it to the view
			var viewModel = new DashboardVM
			{
				Departments = departments,
				QueueServices = queueServices,
				TicketStats = ticketStats
			};

			return View(viewModel);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
