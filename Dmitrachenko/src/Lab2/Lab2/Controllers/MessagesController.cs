using System.Web.Mvc;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;

namespace Lab2.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ICommentService commentService;
        public MessagesController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public ActionResult Create(int id)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            var commentDataModel = new CommentDataModel();
            commentDataModel.AuthorId = currentStudent.Id;
            commentDataModel.PostId = id;
            return View(commentDataModel);
        }

        [HttpPost]
        public ActionResult Create(CommentDataModel commentDataModel, int id)
        {
            var currentStudent = Session["currentStudent"] as StudentDataModel;
            if (currentStudent != null)
                ViewBag.CurrentStudent = currentStudent;
            try
            {
                commentDataModel.AuthorId = currentStudent.Id;
                commentDataModel.PostId = id;
                commentService.Create(commentDataModel);
                return RedirectToAction("Details", "Posts", new { id = commentDataModel.PostId });
                //return RedirectToAction("Posts", "Details", new { id = commentDataModel.PostId });
            }
            catch
            {
                return View(commentDataModel);
            }
        }

        private StudentDataModel SessionCurrentStudent()
        {
            var currentStudent = Session["currentStudent"] as StudentDataModel;
            if (currentStudent != null)
                ViewBag.CurrentStudent = currentStudent;
            return currentStudent;
        }
    }
}
