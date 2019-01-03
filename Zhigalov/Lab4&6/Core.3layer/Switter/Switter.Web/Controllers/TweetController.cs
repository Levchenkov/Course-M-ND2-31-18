using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Switter.Domain.Contracts.Services;
using Switter.Domain.Contracts.ViewModels;

namespace Switter.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITweetService tweetService;
        private readonly IUserService userService;

        public TweetController(ITweetService tweetService, IUserService userService)
        {
            this.tweetService = tweetService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<TweetViewModel> tweets = tweetService.GetAll();
                return View(tweets);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TweetViewModel tweet)
        {
            if (ModelState.IsValid)
            {
                tweet.Author = await userService.FindByNameAsync(HttpContext.User.Identity.Name);
                tweet.AuthorId = tweet.Author.Id;
                tweet.Date = DateTime.Now;
                tweetService.Create(tweet);
                return RedirectToAction("Index", "Tweet");
            }
            return View("Create");
        }
    }
}