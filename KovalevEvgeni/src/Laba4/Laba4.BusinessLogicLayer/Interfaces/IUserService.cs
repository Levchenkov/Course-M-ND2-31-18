using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.BusinessLogicLayer.Infrastructure;
using Laba4.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> CreateAsync(UserViewModel user);
        Task<ClaimsIdentity> AuthenticateAsync(UserViewModel user);
        Task SetInitialData(UserViewModel user, List<string> roles);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task SendEmailAsync(string userId, string subject, string body);
        string UserId { get; set; }

        Task<bool> EmailConfirmation(string userId, string token);
        bool StatusEmailConfirmed { get; set; }
    }
}
