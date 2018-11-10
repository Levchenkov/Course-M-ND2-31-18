
using Newtonsoft.Json;
using OnionApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class HomeController : Controller
    {

        IStudentRepository repo;

        public HomeController(IStudentRepository r)
        {
            repo = r;
        }


        // GET: Home
        public ActionResult Index()
        {
            //
            var students = repo.GetStudentList();
            return View(students);
        }


        public ActionResult Create()
        {
            //
            //Student student = new Student() { Age = 17, FirstName = "Alex", LastName = "M" };
            ////
            //return View(new Student() { Age = student.Age, FirstName = student.FirstName, LastName = student.LastName });
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Create(Student newstudent)
        {
            repo.Create(newstudent);
            repo.Save();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(repo.GetStudent(id));
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            //
            repo.Delete(id);
            repo.Save();
            //
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Details(int id)
        {
            return View(repo.GetStudent(id));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Student student = repo.GetStudent(id);
            return View(student);
        }
        //[HttpPost]
        //public ActionResult Edit(Student editstudent)
        //{
        //    //
        //    //string file_path = Server.MapPath("~/Files/Students.json");
        //    ////
        //    //List<Student> students = System.IO.File.Exists(file_path) ? JsonConvert.DeserializeObject<List<Student>>(System.IO.File.ReadAllText(file_path)) : new List<Student>();
        //    //// 
        //    //List<Student> newstudents = new List<Student>();
        //    ////
        //    //foreach (var item in students)
        //    //{
        //    //    if (item.ID != editstudent.ID) newstudents.Add(item);
        //    //    else newstudents.Add(editstudent);
        //    //}
        //    ////
        //    //System.IO.File.WriteAllText(file_path, JsonConvert.SerializeObject(newstudents));

        //    //return RedirectToAction("Index", "Home");
        //}
    }
}