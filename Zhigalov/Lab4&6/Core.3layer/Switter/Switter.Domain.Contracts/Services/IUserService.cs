using Microsoft.AspNetCore.Identity;
using Switter.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Domain.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserViewModel user);
        Task<bool> SignInUserAsync(UserViewModel user);
        Task<UserViewModel> FindByNameAsync(string name);
        Task<string> GetUserTokenAsync(UserViewModel user);
        Task<UserViewModel> GetByEmailAsync(string email);
        Task<string> GetUserIdAsync(UserViewModel user);
        Task<bool> EmailConfirmedAsync(UserViewModel user, string token);
        Task LogOutAsync();
    }
}
