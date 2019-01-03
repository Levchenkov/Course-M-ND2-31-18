using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Switter.Data.Contracts.Entities;
using Switter.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Data.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> EmailConfirmedAsync(User user, string token)
        {
            User currentUser = await GetByEmailAsync(user.Email);
            IdentityResult result = await userManager.ConfirmEmailAsync(currentUser, token);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(currentUser, isPersistent: true);
            }
            return result;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            User currentUser = await userManager.FindByEmailAsync(email);
            return currentUser;
        }

        public async Task<string> GetUserIdAsync(User user)
        {
            User identity = await GetByEmailAsync(user.Email);
            return identity.Id;
        }

        public async Task<string> GetUserTokenAsync(User user)
        {
            User currentUser = await GetByEmailAsync(user.Email);
            string token = await userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            return token;
        }

        public async Task LogOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<SignInResult> SignInUserAsync(User user, string password)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(user, password, true, false);
            return result;
        }

        public async Task<User> FindByNameAsync(string name)
        {
            User result = await userManager.FindByNameAsync(name);
            return result;
        }

    }
}
