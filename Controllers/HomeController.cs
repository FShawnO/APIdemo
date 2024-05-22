using APIdemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIdemo.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //_context.Addresses;
            return View(_context.Categories.ToList());
        }

        public IActionResult First()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult CheckAccount()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Spots()
        {
            return View();
        }

        public IActionResult JSONTest()
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
