using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModel;
using Microsoft.Owin.Security;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDomainUnitOfWork domainService;
        private int numberOfPosts = 20;

        public HomeController(IDomainUnitOfWork domainService)
        {
            this.domainService = domainService;
        }

        public ActionResult Index()
        {
            var posts = domainService.PostService.GetLastestRecords(numberOfPosts);
            var viewModel = new HomeIndexViewModel() { Posts = posts };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(HomeIndexViewModel model)
        {
            var postViewModel = model.FormPost;
            var posts = domainService.PostService.GetLastestRecords(numberOfPosts);
            model = new HomeIndexViewModel() { Posts = posts };
            if (postViewModel.Content.Length > 240)
            {
                ModelState.AddModelError("", "Message is too long!");
                model.FormPost = postViewModel;
                return RedirectToAction("Index", "Home", model);
            }
            var user = await domainService.UserService.GetByUserName(User.Identity.Name);
            if (user == null)
            {
                AuthenticationManager.SignOut();
                ModelState.AddModelError("", "User not found!");
                model.FormPost = postViewModel;
                return RedirectToAction("Index", "Home", model);
            }
            postViewModel.User = user;
            var result = await domainService.PostService.CreatePostAsync(postViewModel);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(result.Property, result.Message);
                model.FormPost = postViewModel;
            }
            return RedirectToAction("Index", "Home", model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}