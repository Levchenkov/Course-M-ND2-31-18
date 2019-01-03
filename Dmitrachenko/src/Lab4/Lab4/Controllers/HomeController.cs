using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        [Authorize]
        public ActionResult GetPosts()
        {
            List<Post> posts = new List<Post>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                posts = db.Posts.ToList();
            }
            return View(posts);
        }

        [Authorize]
        public ActionResult Create()
        {
            List<Post> posts = new List<Post>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var currentUser = ((ClaimsIdentity)User.Identity).Name; 
                if (currentUser != null)
                    ViewBag.CurrentUser = currentUser;
                posts = db.Posts.ToList();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var currentUser = ((ClaimsIdentity)User.Identity).Name;
                if (currentUser != null)
                    ViewBag.CurrentUser = currentUser;
                post.AuthorName = db.Users.Where(s => s.UserName==currentUser).FirstOrDefault().UserName;
                post.AuthorId = db.Users.Where(s => s.UserName==currentUser).FirstOrDefault().Id;
                post.Created = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("GetPosts", "Home");
            }
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}