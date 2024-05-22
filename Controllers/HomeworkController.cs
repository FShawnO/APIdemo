using Microsoft.AspNetCore.Mvc;

namespace prjDEMO.Controllers
{
    public class HomeworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Homework1()
        {
            return View();
        }
    }
}
