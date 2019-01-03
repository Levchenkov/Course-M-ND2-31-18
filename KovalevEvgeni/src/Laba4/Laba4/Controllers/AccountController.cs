using AutoMapper;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.BusinessLogicLayer.Infrastructure;
using Laba4.BusinessLogicLayer.Interfaces;
using Laba4.Models;
using Laba4.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Laba4.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private IMapper mapper;
        public AccountController()
        {
            mapper = new MapperConfiguration(c => { c.AddProfile<MappingProfileViewModel>(); }).CreateMapper();
        }
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userDetails = new UserViewModel { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthenticateAsync(userDetails);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    if (UserService.StatusEmailConfirmed)
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("Index", "Post");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не подтвержден email.");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Post");
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userDetails = mapper.Map<RegisterModel, UserViewModel>(model);
                OperationDetails operationDetails = await UserService.CreateAsync(userDetails);
                if (operationDetails.Succedeed)
                {
                    var code = await UserService.GenerateEmailConfirmationTokenAsync(UserService.UserId);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = UserService.UserId, code = code },
                               protocol: Request.Url.Scheme);
                    await UserService.SendEmailAsync(UserService.UserId, "Подтверждение электронной почты",
                               "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                               + callbackUrl + "\">завершить регистрацию</a>");
                    return View("SuccessRegister");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            bool result = await UserService.EmailConfirmation(userId, code);
            if (result)
            {
                return View("SuccessEmail");
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { userId = userId });
            }
        }

        public async Task<ActionResult> Confirm(string userId)
        {
            var code = await UserService.GenerateEmailConfirmationTokenAsync(UserService.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = UserService.UserId, code = code },
                       protocol: Request.Url.Scheme);
            await UserService.SendEmailAsync(UserService.UserId, "Подтверждение электронной почты",
                       "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                       + callbackUrl + "\">завершить регистрацию</a>");
            return View();
        }
    }
}