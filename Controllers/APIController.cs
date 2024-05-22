using APIdemo.Models;
using APIdemo.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIdemo.Controllers
{
    public class APIController : Controller
    {        
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public APIController(MyDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        public IActionResult Index()
        {
            //return Content("Hello World!","text/html");
            System.Threading.Thread.Sleep(5000);
            return Content("世界你好!", "text/html", System.Text.Encoding.UTF8);
        }

        //public IActionResult Register(string Username, string Email, string Age)
        public IActionResult Register(Member member, IFormFile avator)
        {
            if(string.IsNullOrEmpty(member.Name)) {
                member.Name = "guest";
            }

            //_context.Members.Add(member);
            //_context.SaveChanges();

            //string info = $"{avator.FileName} - {avator.Length} - {avator.ContentType}";
            //string info = _environment.WebRootPath;
            //string test = _environment.ContentRootPath;
            string uploadPath = Path.Combine(_environment.WebRootPath, "uploads", avator.FileName);

            //string uploadPath = @"D:\Data\code_test\MSIT158\RESTfulAPI_AJAX\APIdemo\wwwroot\uploads\${}";
            using(var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                avator.CopyTo(fileStream);
            }

            byte[] imgByte = null;
            using(var memoryStream = new MemoryStream())
            {
                avator.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }

            member.FileName = avator.FileName;
            member.FileData = imgByte;
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content($"檔案名稱{avator.FileName}", "text/plain", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.Name}! You age {member.Age}, your Email is {member.Email}", "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Accounts(string Account) 
        {
            string message;
            if (string.IsNullOrEmpty(Account))
            {
                message = "請輸入資料！" + Account;
                //var accounts = _context.Members.Select(x => x.Name);
                //return Json(accounts);
                //return NotFound();
            } else
            {
                //xvar account = _context.Members.Any(y => y.Name.Equals(Account));
                var account = _context.Members.Where(y => y.Name.Equals(Account)).Select(x => x.Name);
                if (account.Any())
                {
                    message = "此帳號已存在！";
                    //return Content(message, "text/plain", System.Text.Encoding.UTF8);
                } else
                {
                    message = "此帳號可使用！";
                    //return NotFound();
                }

            }
            return Content(message, "text/plain", System.Text.Encoding.UTF8);
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
                    return NotFound();
                }
            }

            //return Content("<h2>世界你好!</h2>", "text/html", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public IActionResult Spots([FromBody] SearchDTO SearchDTO)
        {
            var spots = SearchDTO.categoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(s => s.CategoryId == SearchDTO.categoryId);
            if (!string.IsNullOrEmpty(SearchDTO.keyword)){
                spots = spots.Where(s => s.SpotTitle.Contains(SearchDTO.keyword) || s.SpotDescription.Contains(SearchDTO.keyword));
            }

            //int totalCount = spots.Count();
            //int pageSize = SearchDTO.pageSize;
            //int page = SearchDTO.page;
            //int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //spots = spots.Skip((page-1)*pageSize).Take(pageSize);

            //SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            //spotsPaging.TotalCount = totalCount;
            //spotsPaging.TotalPages = totalPages;
            return Json(spots);
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
