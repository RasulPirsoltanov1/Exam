using Exam.Core.DTOs;
using Exam.Core.Entities;
using Exam.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemmbersController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        private AppDbContext _context;

        public TeamMemmbersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.TeamMembers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemmberDTO memmber)
        {
            var fileName = Guid.NewGuid().ToString();
            string name=memmber.Photo.FileName;

            var path = Path.Combine(_environment.WebRootPath, "admin/uploads", memmber.Photo.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await memmber.Photo.CopyToAsync(stream);
                stream.Close();
            }
            TeamMemmber newMemmber = new()
            {
                Name = memmber.Name,
                Position = memmber.Position,
                Twitter = memmber.Twitter,
                Instagram = memmber.Instagram,
                Linkedin = memmber.Linkedin,
                Facebook = memmber.Facebook,
                About = memmber.About,
                Photo = fileName+name
            };
            await _context.TeamMembers.AddAsync(newMemmber);
            await _context.SaveChangesAsync();
            return Content("ASdas");
        }
    }
}
