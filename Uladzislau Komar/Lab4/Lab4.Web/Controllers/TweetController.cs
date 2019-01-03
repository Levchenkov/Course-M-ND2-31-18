using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Domain.Contracts.Services;
using Lab4.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITweetService service;
        private readonly IPersonService personService;

        public TweetController(ITweetService service, IPersonService personService)
        {
            this.service = service;
            this.personService = personService;
        }


        // GET: Tweet
        public ActionResult Index()
        {
            var model = service.GetTweetList();
            return View(model);
        }

        // GET: Tweet/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tweet/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                model.Created = DateTime.Now;
                var user = personService
                    .GetCurrentUserAsync(HttpContext).Result;
                model.AuthorId = user.Id;
                model.Author = user;
                service.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tweet/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tweet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}