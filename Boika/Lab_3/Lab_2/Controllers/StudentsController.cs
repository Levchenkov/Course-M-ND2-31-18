using AutoMapper;
using Lab_2.Models;
using Lab_2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_2.Controllers
{
    public class StudentsController : Controller
    {
        private DataBaseContext db;

        public StudentsController()
        {
            db = new DataBaseContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(VMStudent vmStudent)
        {
            Student user = Mapper.Map<VMStudent, Student>(vmStudent);
            user = db.Students.Where(a => a.FirstName == vmStudent.FirstName && a.LastName == vmStudent.LastName).SingleOrDefault();

            if (user == null)
            {
                user = new Student()
                {
                    FirstName = vmStudent.FirstName,
                    LastName = vmStudent.LastName,
                };

                db.Students.Add(user);
                db.SaveChanges();
            }

            Session["User"] = user;
            return RedirectToAction("Index", "Posts");
        }
    }
}