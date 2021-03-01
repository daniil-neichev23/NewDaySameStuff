using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewDaySameStuff.Hubs;
using NewDaySameStuff.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace NewDaySameStuff.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private AppDbContext _context;
        private IWebHostEnvironment _appEnvironment;
        IHubContext<LikeHub> hubContext; 

        public HomeController(UserManager<AppUser> userMgr,
            AppDbContext context,
            IWebHostEnvironment appEnvironment,
            IHubContext<LikeHub> hub)
        {
            userManager = userMgr;
            _context = context;
            _appEnvironment = appEnvironment;
            hubContext = hub;
        }

        [Authorize]
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //dynamic myModel = new ExpandoObject();
            //AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //string message = "Hello " + user.UserName;
            //myModel.String = message;
            //myModel.Posts = _context.Posts.ToList();
            //return View(myModel);
           
            var posts = _context.Posts.ToList();
            return View(posts);
        }
        public ActionResult Like(int id)
        {
            Post update = _context.Posts.ToList().Find(u => u.PostId == id);
            update.Like += 1;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string product)
        {
            await hubContext.Clients.All.SendAsync("Notify", $"Добавлено: {product} - {DateTime.Now.ToShortTimeString()}");
            return RedirectToAction("Index");
        }
    }
}
