using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab1.Models;
using System.IO;

namespace Lab1.Controllers
{
    public class StudentRepositoryController : Controller
    {
        private StudentRepository studentRepository;

        public StudentRepositoryController()
        {
            studentRepository = new StudentRepository();
        }

        public ActionResult Index()
        {
            List<StudentInfo> students = studentRepository.GetStudents();
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentInfo student)
        {
            studentRepository.Create(student);
            var students = studentRepository.GetStudents();
            return View("Index",students);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                studentRepository.Delete(id);
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
            }
            return View("Index", studentRepository.GetStudents());
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            StudentInfo student = null;
            try
            {
                student = studentRepository.GetStudentById(id);
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(StudentInfo student)
        {
            studentRepository.Update(student);

            return View("Index", studentRepository.GetStudents());
        }
    }
}