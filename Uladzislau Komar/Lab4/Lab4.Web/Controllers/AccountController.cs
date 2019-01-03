using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Lab4.Domain.Contracts.ViewModels;
using Lab4.Domain.Contracts.Services;

namespace Lab4.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPersonService service;
        private readonly IEmailService emailService;

        public AccountController(IPersonService service, IEmailService emailService)
        {
            this.service = service;
            this.emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var code = service.GetEmailComfirmationCodeAsync(model).Result;
                var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = model.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    
                var result = await service.Register(model, callbackUrl);
                if (result.Succeeded)
                {
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Confirm Email: <a href='{callbackUrl}'>link</a>");
                    return Content("To complete the registration, check the email and follow the link in the email.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await service.ConfirmEmail(userId, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Tweet");
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new PersonViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.Login(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Tweet");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await service.LogOut();
            return RedirectToAction("Index", "Tweet");
        }
        
    }
}