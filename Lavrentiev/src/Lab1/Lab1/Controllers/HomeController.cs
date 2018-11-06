using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Lab1.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudBase Db;

        public HomeController()
        {
            Db = new StudBase();
        }

        public ActionResult Index()
        {
            return View(Db.StudentList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student st)
        {
            Db.AddStudent(st);
            return RedirectToAction("Index");
        }

        public ActionResult Show()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Show(int id)
        {
            return View("ShowPost", Db.ShowStudent(id));
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Db.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            return View("EditPost", Db.ShowStudent(id));
        }

        [HttpPost]
        public ActionResult EditPost(Student st)
        {
            Db.EditStudent(st);
            return RedirectToAction("Index");
        }
    }
}