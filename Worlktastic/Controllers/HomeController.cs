using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Worlktastic.Models;
using Worlktastic.Data;

namespace Worlktastic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var allJobPosting = _context.JobPosting.ToList();
            return View(allJobPosting);
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

        [HttpGet]
        public IActionResult GetJobPosting(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var jobPostingFromDb = _context.JobPosting.SingleOrDefault(x => x.Id == id);

            if(jobPostingFromDb == null)
            {
                return NotFound();
            }

            return Ok(jobPostingFromDb);
        }
    }
}