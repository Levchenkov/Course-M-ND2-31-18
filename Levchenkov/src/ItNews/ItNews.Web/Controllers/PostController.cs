using ItNews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItNews.Web.Controllers
{
    public class PostController : Controller
    {
        [Authorize(Roles = "Reader")]
        public IActionResult Details(int id)
        {
            var viewModel = new PostViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Reader")]
        public JsonResult DetailsJson(int id)
        {
            return Json("not impl");
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Create()
        {
            var viewModel = new PostCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //exception action filter
        //model state action filter
        [Authorize(Roles = "Writer")]
        public JsonResult Create(PostCreateViewModel viewModel)
        {
            return Json("ok");
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Edit(int id)
        {
            var viewModel = new PostEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //exception action filter
        //model state action filter
        [Authorize(Roles = "Writer")]
        public JsonResult Edit(PostEditViewModel viewModel)
        {
            return Json("ok");
        }
    }
}