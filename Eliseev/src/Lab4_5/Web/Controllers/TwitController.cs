using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class TwitController : Controller
    {
        private ITwitService twitService;

        public TwitController(ITwitService twitService)
        {
            this.twitService = twitService;
        }



        // GET: Twit
        public ActionResult Index()
        {
            ViewBag.NewTwit = new TwitViewModel();
            var twits = twitService.GetLatest(20);
            return View(twits);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Add(TwitViewModel twitViewModel)
        {
            if (twitViewModel.Content.Length > 240)
            {
                ModelState.AddModelError("", "Twit is longer than 240 symbols");
                RedirectToAction("Index");
            }
            twitViewModel.UserId = User.Identity.GetUserId();
            twitViewModel.Id = Guid.NewGuid().ToString();
            await twitService.CreateAsynk(twitViewModel);
            return RedirectToAction("Index");
        }
    }
}