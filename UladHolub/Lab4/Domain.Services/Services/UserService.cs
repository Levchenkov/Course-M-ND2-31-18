using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModel;
using Domain.Services.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserViewModel userViewModel)
        {
            ClaimsIdentity claim = null;
            var user = await unitOfWork.UserManager.FindByEmailAsync(userViewModel.Email);
            if (user == null) { return claim; }
            user = await unitOfWork.UserManager.FindAsync(user.UserName, userViewModel.Password);
            if (user != null)
                claim = await unitOfWork.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<IOperationDetails> CreateAsync(UserViewModel userViewModel)
        {
            User user = await unitOfWork.UserManager.FindByEmailAsync(userViewModel.Email);
            if (user != null)
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
            user = new User { Email = userViewModel.Email, UserName = userViewModel.UserName };
            var result = await unitOfWork.UserManager.CreateAsync(user, userViewModel.Password);
            if (result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            await unitOfWork.UserManager.AddToRoleAsync(user.Id, "User");
            await unitOfWork.SaveAsync();
            return new OperationDetails(true, "Registration successfully completed", "");
        }
        public async Task<bool> SendEmailAsync(string id, string callbackUrl)
        {
            await unitOfWork.UserManager.SendEmailAsync(id, "Email Verification", 
                String.Format("To complete registration, follow the link: <a href=\"{0}\">complete registration</a>", callbackUrl));
            return true;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string id)
        {
            return await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(id);
        }

        public async Task<string> GetIdByEmail(string email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(email);
            return user.Id;
        }

        public async Task<UserViewModel> GetByEmail(string email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(email);
            return DomainMapper.Mapper.Map<User, UserViewModel>(user);
        }

        public async Task<UserViewModel> GetByUserName(string userName)
        {
            var user = await unitOfWork.UserManager.FindByNameAsync(userName);
            return DomainMapper.Mapper.Map<User, UserViewModel>(user);
        }

        public async Task<UserViewModel> GetById(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            return DomainMapper.Mapper.Map<User, UserViewModel>(user);
        }

        public async Task<IOperationDetails> ConfirmEmailAsync(string userId, string code)
        {
            var result = await unitOfWork.UserManager.ConfirmEmailAsync(userId, code);
            if (result.Errors.Count() > 0) { return new OperationDetails(false, result.Errors.FirstOrDefault(), ""); }                
            return new OperationDetails(true, "Email successfully confirmed", "");
        }

        public async Task<bool> CheckEmailConfirm(string email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(email);
            return user.EmailConfirmed;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
