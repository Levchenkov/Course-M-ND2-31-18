using Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IOperationDetails> Create(UserViewModel userViewModel);
        Task<ClaimsIdentity> Authenticate(UserViewModel userViewModel);
        Task SetInitialData(UserViewModel adminViewModel, List<string> roles);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<string> GetIdByEmail(string email);
        Task<bool> SendEmailAsync(string userId, string callbackUrl);
        Task<IOperationDetails> ConfirmEmailAsync(string userId, string token);
     }
}
