using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Switter.Domain.Contracts.Services;
using Switter.Domain.Contracts.ViewModels;

namespace Switter.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            this.userService = userService;
            this.emailService = emailService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool result = await userService.CreateAsync(user);

                if (result)
                {
                    string token = await userService.GetUserTokenAsync(user);

                    string callbackUrl = Url.Link(
                        "default",
                        new { Controller = "User", Action = "Confirm", userEmail = user.Email, code = token });

                    await emailService.SendEmailAsync(user.Email, "Confirm your account",
                        $"Confirm the registration by clicking on the link: <a href='{callbackUrl}'>confirm</a>");

                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Registration");
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(string userEmail, string code)
        {
            UserViewModel user = await userService.GetByEmailAsync(userEmail);
            bool result = await userService.EmailConfirmedAsync(user, code);

            if (result)
            {
                return View("Confirm");
            }
            return RedirectToAction("Registration", "User");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool result = await userService.SignInUserAsync(user);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await userService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}