using APIdemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIdemo.Controllers
{
    public class APIController : Controller
    {        
        private readonly MyDbContext _context;

        public APIController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //return Content("Hello World!","text/html");
            System.Threading.Thread.Sleep(5000);
            return Content("世界你好!", "text/html", System.Text.Encoding.UTF8);
        }

        public IActionResult Register(int id, string name, int age = 20)
        {
            if(string.IsNullOrEmpty(name)) {
                name = "guest";
            }
            return Content($"{id} - {name}, Hello! you age {age}","text/html", System.Text.Encoding.UTF8);
        }

        public IActionResult Cities(string whereClause, int ClauseID)
        {
            if (string.IsNullOrEmpty(whereClause)) {
                var cities = _context.Addresses.Select(x => x.City).Distinct();
                return Json(cities);
            } else
            {
                if (ClauseID == 1)
                {
                    var sites = _context.Addresses.Where(y => y.City.Equals(whereClause)).Select(x => x.SiteId).Distinct();
                    return Json(sites);
                } else if (ClauseID == 2)
                {
                    var roads = _context.Addresses.Where(y => y.SiteId.Equals(whereClause)).Select(x => x.Road).Distinct();
                    return Json(roads);
                } else
                {
                    throw new Exception();
                }
            }

            //return Content("<h2>世界你好!</h2>", "text/html", System.Text.Encoding.UTF8);
        }

        public IActionResult Avator(int id)
        {
            //Queryable: 多筆資料; Find: 索引鍵，單筆
            Member? member = _context.Members.FirstOrDefault(m=>m.MemberId==id);    
            if(member != null) {
                byte[] img = member.FileData;
                if(img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
    }
}
