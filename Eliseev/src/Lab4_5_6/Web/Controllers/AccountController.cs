using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService userService;       

        private IAuthenticationManager authenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = new UserViewModel() { Email = model.Email, UserName = model.Email, Password = model.Password };
                ClaimsIdentity claim = await userService.Authenticate(userViewModel);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
                else
                {
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false                        
                    },claim);
                    return RedirectToAction("Index","Twit");
                }
            }
            return View(model);
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = new UserViewModel() { Email = model.Email, UserName = model.Email, Password = model.Password };
                IOperationDetails result = await userService.Create(userViewModel);
                if (result.Succeded)
                {
                    string id = await userService.GetIdByEmail(model.Email);
                    string token = await userService.GenerateEmailConfirmationTokenAsync(id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = id, code = token }, protocol: Request.Url.Scheme);

                    await userService.SendEmailAsync(id, callbackUrl);

                    return RedirectToAction("Confirm", "Account", new { Email = userViewModel.Email });
                }
                else
                {
                    ModelState.AddModelError(result.Property, result.Message);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await userService.ConfirmEmailAsync(userId, code);
            return View(result.Succeded ? "ConfirmEmail" : "Error");
        }




        public ActionResult Main()
        {
            return View();
        }

        private async Task SetInitialDataAsync()
        {
            await userService.SetInitialData(new UserViewModel()
            {
                Email ="admin@mail.ru",
                UserName = "admin@mail.ru",
                Password = "qwerty123456"                
            }, new List<string>() { "User", "Admin" });
        }

    }
}