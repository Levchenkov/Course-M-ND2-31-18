using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Repositories;
using Twitter.Domain.Contracts.InfoModels;

namespace Twitter.Domain.Contracts.Services
{
    public class UserService : IUserService<IUserInfo>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository<ApplicationUser> userRepository;

        public UserService(IUserRepository<ApplicationUser> userRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> AddUserAsync(IUserInfo userInfo)
        {
            var item = Mapper.Map<IUserInfo, ApplicationUser>(userInfo);
            return await userRepository.CreateAsync(item, userInfo.Password);
        }
        public bool CheckEmail(string email)
        {
            return userRepository.IsEmailInUse(email);
        }

        public bool CheckUserName(string userName)
        {
            return userRepository.IsUserNameInUse(userName);
        }

        public async Task<string> GetUserTokenAsync(IUserInfo userInfo)
        {
            var item = mapper.Map<IUserInfo, ApplicationUser>(userInfo);
            return await userRepository.GetUserTokenAsync(item);
        }

        public async Task<string> GetId(IUserInfo userInfo)
        {
            var item = mapper.Map<IUserInfo, ApplicationUser>(userInfo);
            return await userRepository.GetUserIdAsync(item);
        }

        public async Task<bool> ConfirmUserEmail(IUserInfo userInfo, string token)
        {
            var applicationUser = mapper.Map<IUserInfo, ApplicationUser>(userInfo);
            var result = await userRepository.EmailConfirmedAsync(applicationUser, token);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SignInAsync(IUserInfo userInfo)
        {
            var item = await userRepository.GetByEmailAsync(userInfo.Email);
            var result = await userRepository.
                SignInUserAsync(item, userInfo.Password, userInfo.RememberMe);
            return result.Succeeded ? true : false;
        }

        public async Task LogOut()
        {
            await userRepository.LogOut();
        }
    }
}
