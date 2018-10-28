using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class StudentController : Controller
    {
        private IRepository _repository;

        public StudentController()
        {
            _repository = new JsonStudentRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Message = String.Empty;
            List<JsonStudent> jsonStudentList = _repository.Get();
            return View(jsonStudentList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = String.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Create(JsonStudent student)
        {
            _repository.Create(student);
            ViewBag.Message = "Added successfully!";
            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            ViewBag.Message = String.Empty;
            List<JsonStudent> jsonStudentList = _repository.Get();
            return View(jsonStudentList);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            List<JsonStudent> jsonStudentList = _repository.Get();
            ViewBag.Message = "Deleted Succesfully";
            return View(jsonStudentList);
        }

        /// <summary>
        /// Display a listbox for selecting students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update()
        {
            ViewBag.Message = String.Empty;
            List<JsonStudent> jsonStudentList = _repository.Get();
            return View("SelectStudent", jsonStudentList);
        }

        /// <summary>
        /// Find selected student and goes over to change form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("LoadStudentForUpdate")]
        public ActionResult Update(int id)
        {
            ViewBag.Message = String.Empty;
            List<JsonStudent> jsonStudentList = _repository.Get();
            var student = jsonStudentList.Find(x => x.Id == id);
            return View("Update", student);
        }

        /// <summary>
        /// Update student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(JsonStudent student)
        {
            _repository.Update(student);
            List<JsonStudent> jsonStudentList = _repository.Get();
            ViewBag.Message = "Updated succesfully";
            return View("SelectStudent", jsonStudentList);
        }
    }
}