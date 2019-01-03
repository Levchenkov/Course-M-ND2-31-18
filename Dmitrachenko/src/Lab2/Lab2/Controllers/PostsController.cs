using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;

namespace Lab2.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService post)
        {
            postService = post;
        }

        public ActionResult Index(int? id)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            var modelTuple = new Tuple<StudentDataModel, IEnumerable<PostDataModel>>(currentStudent, postService.GetAllPosts());
            return View(modelTuple);
        }

        public ActionResult Details(int id)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            var postDataModel = postService.Get(id);
            return View(postDataModel);
        }

        public ActionResult Create()
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            return View(new CreatePostDataModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostDataModel createPostDataMode)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            createPostDataMode.AuthorId = currentStudent.Id;
            postService.Create(createPostDataMode);
            return RedirectToAction("Index", "Posts", currentStudent);
        }

        public ActionResult Edit(int id)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            var postDataModel = postService.Get(id);
            var createPostDataModel = postService.Get(postDataModel);
            return View(createPostDataModel);
        }

        [HttpPost]
        public ActionResult Edit(CreatePostDataModel createPostDataModel)
        {
            StudentDataModel currentStudent = SessionCurrentStudent();
            postService.Edit(createPostDataModel);
            return RedirectToAction("Index", "Posts");
        }

        public ActionResult Delete(int id)
        {
            if (id != null && id != 0)
            {
                StudentDataModel currentStudent = SessionCurrentStudent();
                var postDataModel = postService.Get(id);
                return View(postDataModel);
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            postService.Delete(id);
            return RedirectToAction("Index", "Posts");
        }

        protected override void Dispose(bool disposing)
        {
            postService.Dispose();
            base.Dispose(disposing);
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
