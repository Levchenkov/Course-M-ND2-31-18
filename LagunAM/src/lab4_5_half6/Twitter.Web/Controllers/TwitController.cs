using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitter.Domain.Contracts.InfoModels;
using Twitter.Domain.Contracts.Services;
using Twitter.Web.ViewModels.Twit;

namespace Twitter.Web.Controllers
{
    [Authorize]
    public class TwitController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITwitService twitService;

        public TwitController(ITwitService twitService, IMapper mapper)
        {
            this.mapper = mapper;
            this.twitService = twitService;
        }

        [HttpGet]
        public JsonResult CheckDuplicateString(string addedTags)
        {
            List<string> Tags = addedTags.Split(' ').ToList();
            Tags.RemoveAll(p => p ==" ");
            Tags.RemoveAll(p => p == "");
            HashSet<string> hashSet = new HashSet<string>();
            foreach (string Now in Tags)
            {
                if (!hashSet.Add(Now))
                    return Json(false);
            }
            return Json(true); ;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            TwitView twitView = new TwitView();
            var items = twitService.GetPackTwits(10);
            twitView.Twits = items;
            return View(twitView);
        }
        
    }
}