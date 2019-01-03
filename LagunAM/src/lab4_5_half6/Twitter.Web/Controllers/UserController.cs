using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twitter.Data.Contracts.Entities;
using Twitter.Domain.Contracts.InfoModels;
using Twitter.Domain.Contracts.Services;

using Twitter.Web.ViewModels.User;

namespace Twitter.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService<IUserInfo> userService;
        private readonly IMapper mapper;
        private readonly IEmailSenderService emailSenderService;
        public UserController(IUserService<IUserInfo> userService,
            IMapper mapper, IEmailSenderService emailSenderService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.emailSenderService = emailSenderService;
        }

        [HttpGet]
        public JsonResult CheckUserName(string userName)
        {
            var item = Json(userService.CheckUserName(userName));
            return item;
        }

        [HttpGet]
        public JsonResult CheckEmail(string email)
        {
            var item = Json(userService.CheckEmail(email));
            return item;
        }

        [HttpGet]
        public IActionResult Registration(UserInfo userInfo)
        {
            var item = mapper.Map<UserInfo, RegistrationView>(userInfo);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationView registrationView)
        {
            if (ModelState.IsValid)
            {
                var item = mapper.Map<RegistrationView, IUserInfo>(registrationView);
                var result = await userService.AddUserAsync(item);
                if (result.Succeeded)
                {
                    var token = await userService.GetUserTokenAsync(item);


                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "user",
                        new { item.Email, token },
                        protocol: HttpContext.Request.Scheme);

                    var message = string.Format("Для завершения регистрации перейдите по ссылке:" +
                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>", callbackUrl);

                    await emailSenderService.SendEmailAsync(item.Email, "Registration TwitNew", message);
                    return RedirectToAction("Confirm", "User", new { message = "send confirmed message" } );
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(registrationView);
        }

        [AllowAnonymous]
        public string Confirm(string message)
        {
            Response.Headers.Add("REFRESH", "3;URL=/user/login");
            return $"Email is {message}";
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(RegistrationView registrationView, string token)
        {
            var item = Mapper.Map<RegistrationView, IUserInfo>(registrationView);
            if (await userService.ConfirmUserEmail(item, token))
            {
                

                return RedirectToAction("Confirm", "user", new{ message = "confirmed" } );

            }
            else
            {
                
                return RedirectToAction("Confirm", "user", new { message = "not confirmed" });
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginView loginView) 
        {
            if (ModelState.IsValid)
            {
                var userInfo = mapper.Map<LoginView, IUserInfo>(loginView);
                var result = await userService.SignInAsync(userInfo);
                return RedirectToAction("Index", "Twit");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await userService.LogOut();
            return RedirectToAction("Login", "User");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}