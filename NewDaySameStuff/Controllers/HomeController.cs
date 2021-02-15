using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewDaySameStuff.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDaySameStuff.Controllers
{
    public class HomeController:Controller
    {
        private UserManager<AppUser> userManager;
        private AppDbContext _context;
        private IWebHostEnvironment _appEnvironment;

        public HomeController(UserManager<AppUser> userMgr,
            AppDbContext context,
            IWebHostEnvironment appEnvironment)
        {
            userManager = userMgr;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            dynamic myModel = new ExpandoObject();
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;
            myModel.String = message;
            myModel.Posts = _context.Posts.ToList();
            return View(myModel);
        }
    }
}
