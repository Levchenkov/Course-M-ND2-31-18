using System.Web.Mvc;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService studentService;

        public HomeController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index(int? id)
        {
            if (id != null && id != 0)
            {
                SessionCurrentStudent();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StudentDataModel studentDataModel)
        {
            var currentStudentDataModel = studentService.Get(studentDataModel);
            if (currentStudentDataModel == null)
            {
                var IdStudent = studentService.Create(studentDataModel);
                var currentStudent = studentService.Get(IdStudent);
                Session["currentStudent"] = currentStudent;
                //ViewBag.Message = "Новый пользователь";
                return RedirectToAction("Index", "Posts");
            }
            else if (currentStudentDataModel != null)
            {
                Session["currentStudent"] = currentStudentDataModel;
                //ViewBag.Message = "Пользователь уже зарегистрирован";
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        private void SessionCurrentStudent()
        {
            var currentStudent = Session["currentStudent"] as StudentDataModel;
            if (currentStudent != null)
                ViewBag.CurrentStudent = currentStudent;
        }
    }
}