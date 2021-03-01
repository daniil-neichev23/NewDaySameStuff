using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewDaySameStuff.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDaySameStuff.Controllers
{
    public class PostController:Controller
    {
        private AppDbContext _context;

        private IWebHostEnvironment _appEnvironment;
        private IHostingEnvironment _hostingEnvironment;

        public PostController(AppDbContext context,
            IWebHostEnvironment appEnvironment,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult Index()
        {
            return View(_context.Posts.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                int like = 0;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "Images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
                Post post = new Post
                {
                    ProfilePicture = model.ProfilePicture,
                    Path = uniqueFileName,
                    Like = like,
                };
                _context.Add(post);
                _context.SaveChanges(); 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //public async Task<IActionResult> Create(
        //    [Bind(include:"PostId,ProfilePicture,Path,Like")] Post post,
        //    IFormFile uploadedFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (uploadedFile != null)
        //        {
        //            string path = "/Images/" + uploadedFile.FileName;
        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //            {
        //                await uploadedFile.CopyToAsync(fileStream);
        //            }
        //            int like = 0;
        //            post = new Post { ProfilePicture = uploadedFile.FileName, Path = path, Like = like };
        //            _context.Posts.Add(post);
        //            _context.SaveChanges();
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return View();
        //}
        //public ActionResult Like(int id)
        //{
        //    Post update = _context.Posts.ToList().Find(u => u.PostId == id);
        //    update.Like += 1;
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
