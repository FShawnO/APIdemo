using Microsoft.AspNetCore.Mvc;

namespace APIdemo.Controllers
{
    public class CallAPIController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
