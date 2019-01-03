using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModel;
using Domain.Services.Infrastructure;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IDomainUnitOfWork domainService;

        public AccountController(IDomainUnitOfWork domainService)
        {
            this.domainService = domainService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var claim = await domainService.UserService.AuthenticateAsync(model);
            if (claim == null)
            {
                ModelState.AddModelError("", "Wrong login or password.");
                return View(model);
            }
            var emailConfirm = await domainService.UserService.CheckEmailConfirm(model.Email);
            if (!emailConfirm)
            {
                ModelState.AddModelError("", "Email not Confirmed");
                return View(model);
            }
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var operationDetails = await domainService.UserService.CreateAsync(model);
            if (!operationDetails.Succeeded)
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return View(model);
            }
            model.Id = await domainService.UserService.GetIdByEmail(model.Email);
            var code = await domainService.UserService.GenerateEmailConfirmationTokenAsync(model.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = model.Id, code = code },
                       protocol: Request.Url.Scheme);
            await domainService.UserService.SendEmailAsync(model.Id, callbackUrl);
            return View("~/Views/Account/DisplayEmail.cshtml");
            //if (operationDetails.Succedeed) { return RedirectToAction("Index", "Home"); }                
            //ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            //return View(model);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await domainService.UserService.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "ConfirmEmailError");
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