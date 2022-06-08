using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worlktastic.Data;
using Worlktastic.Models;

namespace Worlktastic.Controllers
{
    [Authorize]
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jobPostingFromDb = _context.JobPosting.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
            return View(jobPostingFromDb);
        }

        public IActionResult CreateEdtitJobPosting(int id)
        {
            if(id != 0)
            {
                var jobPostingFromDb = _context.JobPosting.SingleOrDefault(x => x.Id == id);

                if(jobPostingFromDb.OwnerUsername != User.Identity.Name)
                {
                    return Unauthorized();
                }

                if(jobPostingFromDb != null)
                {
                    return View(jobPostingFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
        {
            jobPosting.OwnerUsername = User.Identity.Name;
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    jobPosting.CompanyImage = bytes;
                }
            }

            // TODO: write jobposting to db
            if (jobPosting.Id == 0)
            {
                // Add new job if not editing
                _context.JobPosting.Add(jobPosting);
            }
            else
            {
                var jobFromDb = _context.JobPosting.SingleOrDefault(x => x.Id == jobPosting.Id);

                if (jobFromDb == null)
                {
                    return NotFound();
                }

                jobFromDb.CompanyImage = jobPosting.CompanyImage;
                jobFromDb.CompanyName = jobPosting.CompanyName;
                jobFromDb.ContactMail = jobPosting.ContactMail;
                jobFromDb.ContactPhone = jobPosting.ContactPhone;
                jobFromDb.ContactWebsite = jobPosting.ContactWebsite;
                jobFromDb.Description = jobPosting.Description;
                jobFromDb.JobLoction = jobPosting.JobLoction;
                jobFromDb.JobTitle = jobPosting.JobLoction;
                jobFromDb.Salary = jobPosting.Salary;
                jobFromDb.StartDate = jobPosting.StartDate;
                jobFromDb.OwnerUsername = jobPosting.OwnerUsername;
            }          
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteJobPostingById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var jobPostingFromDb = _context.JobPosting.SingleOrDefault(x => x.Id == id);

            if (jobPostingFromDb == null)
            { 
                return NotFound(); 
            }

            _context.JobPosting.Remove(jobPostingFromDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
