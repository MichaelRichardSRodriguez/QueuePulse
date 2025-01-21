using Microsoft.AspNetCore.Mvc;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services;
using QueuePulse.Models;
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
