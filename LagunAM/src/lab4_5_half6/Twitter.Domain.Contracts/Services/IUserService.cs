using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Contracts.Services
{
    public interface IUserService<in TModel>
    {
        bool CheckUserName(string userName);
        bool CheckEmail(string email);
        Task<IdentityResult> AddUserAsync(TModel Model);
        Task<string> GetId(TModel userInfo);
        Task<string> GetUserTokenAsync(TModel userInfo);
        Task<bool> ConfirmUserEmail(TModel userInfo, string token);
        Task<bool> SignInAsync(TModel userInfo);
        Task LogOut();

    }
}
