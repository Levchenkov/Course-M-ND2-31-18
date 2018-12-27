using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab_2.Models;

namespace Lab_2.Controllers
{
    public class CommentsController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        public ActionResult Index(int id)
        {
            var comments = db.Posts.Include(a => a.Comments).Include(a => a.Author).Where(a => a.Id == id).Single().Comments;
            return View(comments.ToList());
        }

        public ActionResult Details(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        public ActionResult Create(int id)
        {
            var post = db.Posts.Include(a => a.Comments).Where(a => a.Id == id).Single();
            Comment comment = new Comment();
            comment.PostId = post.Id;
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Created = DateTime.Now;
                comment.Author = db.Students.Find((Session["User"] as Student).Id);
                comment.AuthorId = comment.Author.Id;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = comment.PostId });
            }

            return View(comment);
        }

        public ActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            db.Comments.Attach(comment);
            var entry = db.Entry(comment);
            entry.Property(a => a.Content).IsModified = true;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = comment.PostId });
        }

        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = comment.PostId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
