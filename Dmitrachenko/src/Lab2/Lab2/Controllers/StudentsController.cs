using System.Web.Mvc;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;

namespace Lab2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;


        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index()
        {
            SessionCurrentStudent();
            var listStudents = studentService.GetAllStudents();
            return View(listStudents);
        }

        public ActionResult Details(int id)
        {
            if (id != null && id != 0)
            {
                SessionCurrentStudent();
                var studentViewModel = studentService.Get(id);
                var createStudentViewModel = studentService.Get(studentViewModel);
                return View(createStudentViewModel);
            }
            return View();
        }

        public ActionResult Create()
        {
            SessionCurrentStudent();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentDataModel studentDataModel)
        {
            var currentStudentDataModel = studentService.Get(studentDataModel);
            if (currentStudentDataModel == null)
            {
                var IdStudent = studentService.Create(studentDataModel);
                //ViewBag.Message = "Новый пользователь";
                return RedirectToAction("Index", "Students");
            }
            else if (currentStudentDataModel != null)
            {
                //ViewBag.Message = "Пользователь уже зарегистрирован";
                return RedirectToAction("Index", "Students");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (id != null && id != 0)
            {
                SessionCurrentStudent();
                var studentViewModel = studentService.Get(id);
                var createStudentViewModel = studentService.Get(studentViewModel);
                return View(createStudentViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDataModel studentDataModel)
        {
            studentService.Edit(studentDataModel);
            return RedirectToAction("Index", "Students");
        }

        public ActionResult Delete(int id)
        {
            if (id != null && id != 0)
            {
                SessionCurrentStudent();
                var studentViewModel = studentService.Get(id);
                var deleteStudentViewModel = studentService.Get(studentViewModel);
                return View(deleteStudentViewModel);
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            studentService.Delete(id);
            return Redirect("/Home/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studentService.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SessionCurrentStudent()
        {
            var currentStudent = Session["currentStudent"] as StudentDataModel;
            if (currentStudent != null)
                ViewBag.CurrentStudent = currentStudent;
        }
    }
}
