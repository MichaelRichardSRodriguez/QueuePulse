using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.Utility;

namespace QueuePulse.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]

    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ticket =  _context.Tickets.ToList();
            return View(ticket);
        }

    }
}
