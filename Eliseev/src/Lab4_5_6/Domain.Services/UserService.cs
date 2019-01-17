using Data.Repositories.Entities;
using Data.Repositories.Interfaces;
using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Domain.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userViewModel)
        {
            ClaimsIdentity claims = null;
            User user = await unitOfWork.UserManager.FindAsync(userViewModel.Email, userViewModel.Password);
            if (user != null)
            {
                claims = await unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claims;
        }

        public async Task<IOperationDetails> Create(UserViewModel userViewModel)
        {
            User user = await unitOfWork.UserManager.FindByEmailAsync(userViewModel.Email);
            if (user==null)
            {
                user = new User() { Email = userViewModel.Email, UserName = userViewModel.Email };
                var result = await unitOfWork.UserManager.CreateAsync(user, userViewModel.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await unitOfWork.UserManager.AddToRoleAsync(user.Id, "User");
                await unitOfWork.SaveAsynk();
                return new OperationDetails(true, "Registration completed", "");
            }
            else
            {
                return new OperationDetails(false, "User has already registred with this name", "");
            }
        }

        public async Task SetInitialData(UserViewModel adminViewModel, List<string> roles)
        {
            foreach(string roleName in roles)
            {
                var role = await unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new Role() { Name = roleName };
                    await unitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminViewModel);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            return await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task<string> GetIdByEmail(string email)
        {
            User user = await unitOfWork.UserManager.FindByEmailAsync(email);
            return user.Id;
        }

        public async Task<bool> SendEmailAsync(string userId, string callbackUrl)
        {
            await unitOfWork.UserManager.SendEmailAsync(userId ,"Confirm your Email", $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>");
            return true;
        }

        public async Task<IOperationDetails> ConfirmEmailAsync(string userId,string token)
        {
            IdentityResult result = await unitOfWork.UserManager.ConfirmEmailAsync(userId, token);
            if (result.Errors.Count() > 0)
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }
            return new OperationDetails(true,"Email is confirmed","");
        }


        public void Dispose()
        {
            unitOfWork.Dispose();
        }        
    }
}
