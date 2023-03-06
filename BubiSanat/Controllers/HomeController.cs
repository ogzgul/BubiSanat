using BubiSanat.Data;
using BubiSanat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BubiSanat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(short? id = null)
        {
            IQueryable<Post> applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.Author).Include(p => p.NextPost).Include(p => p.PreviousPost).OrderByDescending(p => p.CreatedAt);
            if (id != null)
            {
                applicationDbContext = applicationDbContext.Where(p => p.CategoryId == id.Value);
            }
            return View( applicationDbContext.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult UserAgreement()
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