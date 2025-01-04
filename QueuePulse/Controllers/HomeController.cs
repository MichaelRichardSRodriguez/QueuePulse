using Microsoft.AspNetCore.Mvc;
using QueuePulse.Data;
using QueuePulse.Models;
using System.Diagnostics;

namespace QueuePulse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
