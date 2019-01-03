using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Twitter.Api.Infrastructure;
using Twitter.Api.Models;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        // POST api/login
        [HttpPost("/api/login")]
        public async Task PostLoginAsync()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == email);

                var response = new
                {
                    accessToken = GenerateJwtToken(email, appUser),
                    userName = appUser.UserName
                };
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                return;
            }
            Response.StatusCode = 400;
            await Response.WriteAsync("Invalid username or password.");
        }

        // POST api/register
        [HttpPost("/api/register")]
        public async Task PostRegisterAsync()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var confirmPassword = Request.Form["confirmPassword"];

            if (password != confirmPassword)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Passwords do not match.");
                return;
            }

            User user = new User { Email = email, UserName = email };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                Response.StatusCode = 200;
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = "http://localhost:49720/confirm.html?userId=" + user.Id + "&code=" + code;              
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Confirm the registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
                return;
            }
            Response.StatusCode = 400;
            await Response.WriteAsync("Something went wrong.");
            return;
        }

        // GET api/confirm
        [HttpGet("/api/confirm")]
        public async Task Confirm(string userId, string code)
        {
            string correctCode = code.Replace(' ', '+');
            if (userId == null || correctCode == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Something went wrong.");
                return;
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("This user is missing.");
                return;
            }
            var result = await userManager.ConfirmEmailAsync(user, correctCode);

            if (result.Succeeded)
            {
                Response.StatusCode = 200;
                await Response.WriteAsync("Email confirmed.");
                return;
            }
            else
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Failed to confirm email.");
                return;
            }
        }

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}