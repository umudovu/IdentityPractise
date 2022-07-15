using IdentityPractise.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityPractise.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;


        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }
    }
}
