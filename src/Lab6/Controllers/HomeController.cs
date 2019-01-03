using Lab4.Infrastructure;
using Lab4.Models;
using Lab4.MyService.Domain.Interface;
using Lab4.MyService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    [MyAuthorize]
    public class HomeController : Controller
    {

        IRepository<Post> repository = new PostDbRepository();

        public ActionResult Index()
        {
            var posts = repository.GetAll();
            return View(posts);

        }

        public ActionResult Create()
        {
            Post post = new Post() { Content = "New Content", Created = DateTime.Now, Author = Session["UserName"].ToString() };

            return View(post);
        }
        [HttpPost]
        public ActionResult Create(Post model)
        {
            if (ModelState.IsValid)
            {
                model.Created = DateTime.Now;
                repository.Create(model);
                repository.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Post post = repository.Get(id.Value);

            return View(post);
        }
        [HttpPost]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {

                repository.Update(model);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Details(int id)
        {
            Post post = repository.Get(id);
            return View(post);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            ChatUser user = new ChatUser() { UserName = Session["UserName"].ToString() };
            return View(user);
        }
    }
}