using System.Threading.Tasks;
using Lab4.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Lab4.Domain.Contracts.Services
{
    public interface IPersonService
    {
        Task<SignInResult> Login(PersonViewModel model);
        Task LogOut();
        Task<IdentityResult> Register(PersonViewModel model, string callbackUrl);
        Task<PersonViewModel> GetCurrentUserAsync(HttpContext context);
        Task<string> GetEmailComfirmationCodeAsync(PersonViewModel model);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
    }
}