using Microsoft.AspNetCore.Identity;
using Switter.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Data.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<SignInResult> SignInUserAsync(User user, string password);
        Task<User> FindByNameAsync(string name);
        Task<string> GetUserTokenAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<string> GetUserIdAsync(User user);
        Task<IdentityResult> EmailConfirmedAsync(User user, string token);
        Task LogOutAsync();
    }
}
