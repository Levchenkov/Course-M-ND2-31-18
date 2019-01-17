using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Hubs;

namespace Web.Controllers
{
    [System.Web.Mvc.Authorize]
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
        public ActionResult GetTwits()
        {
            ViewBag.NewTwit = new TwitViewModel();
            var twits = twitService.GetLatest(20);
            return View("Index",twits);
        }

        [HttpPost]
        public async Task<ActionResult> Add(TwitViewModel twitViewModel)
        {            
            var twit = twitViewModel;
            twit.UserId = User.Identity.GetUserId();
            twit.Id = Guid.NewGuid().ToString();

            await twitService.CreateAsynk(twit);

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TwitHub>();
           
            context.Clients.All.addNewTwit("Refresh");
            return Json(true);
        }
    }
}