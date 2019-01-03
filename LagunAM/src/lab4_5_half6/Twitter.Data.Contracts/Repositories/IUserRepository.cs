using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Data.Contracts.Repositories
{
    public interface IUserRepository<TModel>
    {
        Task<IdentityResult> CreateAsync(TModel model, string password);
        Task<SignInResult> SignInUserAsync(TModel model, string password, bool rememberMe);
        Task<IdentityResult> UpdateAsync(TModel model);
        Task<IdentityResult> DeleteAsync(TModel model);
        bool IsEmailInUse(string Email);
        bool IsUserNameInUse(string UserName);
        Task<string> GetUserTokenAsync(TModel model);
        Task<TModel> GetByEmailAsync(string email);
        Task<string> GetUserIdAsync(TModel model);
        Task<IdentityResult> EmailConfirmedAsync(ApplicationUser model, string token);
        Task LogOut();
    }
}
