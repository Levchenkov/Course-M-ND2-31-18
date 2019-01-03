using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Contracts.Context;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Repositories;

namespace Twitter.Repositories
{
    public class UserRepository : IUserRepository<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public bool IsEmailInUse(string email)
        {
            var applicationUser = userManager.FindByEmailAsync(email);
            return (applicationUser == null) ? false : true;
        }

        public bool IsUserNameInUse(string UserName)
        {
            var applicationUser = userManager.FindByNameAsync(UserName);
            return (applicationUser == null) ? false : true;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser model, string password)
        {
            var item = await userManager.CreateAsync(model, password);
            return item;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser model)
        {
            return await userManager.DeleteAsync(model);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var applicationUser = await userManager.FindByEmailAsync(email);
            return applicationUser;
        }

        public async Task<SignInResult> SignInUserAsync (ApplicationUser applicationUser,
            string password, bool rememberMe)
        {


            var result = await signInManager.PasswordSignInAsync(applicationUser, password,
               rememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser model)
        {
            return await userManager.UpdateAsync(model);
        }

        public async Task<string> GetUserTokenAsync(ApplicationUser model)
        {
            var user = await GetByEmailAsync(model.Email);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<string> GetUserIdAsync(ApplicationUser model)
        {
            var item = await GetByEmailAsync(model.Email);
            return item.Id;
        }

        public async Task<IdentityResult> EmailConfirmedAsync(ApplicationUser model, string token)
        {
            var user = await GetByEmailAsync(model.Email);
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                
            }
            return result;
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
