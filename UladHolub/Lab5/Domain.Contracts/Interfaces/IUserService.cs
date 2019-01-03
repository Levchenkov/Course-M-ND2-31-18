using Domain.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<ClaimsIdentity> AuthenticateAsync(UserViewModel userViewModel);
        Task<IOperationDetails> ConfirmEmailAsync(string userId, string code);
        Task<IOperationDetails> CreateAsync(UserViewModel userViewModel);
        Task<string> GenerateEmailConfirmationTokenAsync(string id);
        Task<string> GetIdByEmail(string email);
        Task<UserViewModel> GetByEmail(string email);
        Task<UserViewModel> GetByUserName(string userName);
        Task<UserViewModel> GetById(string id);
        Task<bool> CheckEmailConfirm(string email);
        Task<bool> SendEmailAsync(string id, string callbackUrl);
    }
}
