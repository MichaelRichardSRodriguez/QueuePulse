using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.Models.Entities;
using QueuePulse.Models.ViewModels;
using QueuePulse.Utility;

namespace QueuePulse.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context; 

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch Departments and Queue Services
            var departments = _context.Departments.ToList();
            var queueServices = _context.QueueServices.Include(q => q.Department).ToList();

            // Fetch Ticket Statuses
            var ticketStats = new TicketStat
            {
                New = _context.Tickets.Count(t => t.Status == StaticDetails.QUEUE_NEW),
                InProgress = _context.Tickets.Count(t => t.Status == StaticDetails.QUEUE_INPROGRESS),
                Completed = _context.Tickets.Count(t => t.Status == StaticDetails.QUEUE_COMPLETED)
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
    }
}
