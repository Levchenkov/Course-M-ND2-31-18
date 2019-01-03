using Lab3mvc.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService studentService;

        public HomeController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public string Index()
        {
            return studentService.MakeSmthWithRepository();
        }

        public string About()
        {
            var result = studentService.Add();
            return result;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}