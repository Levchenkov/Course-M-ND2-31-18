using AutoMapper;
using Data.Contracts.Models;
using Data.Implementation;
using Domain.Implementation.Service;
using DomainContracts.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<UserEntity> userManager;
        private readonly TweetService tweetService;

        public HomeController(ApplicationDbContext context, UserManager<UserEntity> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.tweetService = new TweetService(context, mapper);
        }

        public IActionResult Index()
        {
            var tweets = tweetService.Get();
            return View(tweets);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TweetViewModel tweet)
        {
            var userId = userManager.GetUserId(User);
            tweet.AuthorId = userId;
            tweetService.Create(tweet);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            tweetService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var tweet = tweetService.GetsById(id);
            return View(tweet);
        }
        [HttpPost]
        public IActionResult Edit(TweetViewModel tweet)
        {
            tweetService.Edit(tweet);
            return RedirectToAction("Index");
        }
    }
}
