using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Switter.Data.Contracts.Entities;
using Switter.Data.Contracts.Repositories;
using Switter.Domain.Contracts.Services;
using Switter.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Domain.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateAsync(UserViewModel user)
        {
            User userEntity = mapper.Map<User>(user);
            IdentityResult result = await userRepository.CreateAsync(userEntity, user.Password);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> EmailConfirmedAsync(UserViewModel user, string token)
        {
            User userEntity = mapper.Map<User>(user);
            IdentityResult result = await userRepository.EmailConfirmedAsync(userEntity, token);
            return result.Succeeded ? true : false;
        }

        public async Task<UserViewModel> GetByEmailAsync(string email)
        {
            User userEntity = await userRepository.GetByEmailAsync(email);
            UserViewModel user = mapper.Map<UserViewModel>(userEntity);
            return user;
        }

        public async Task<string> GetUserIdAsync(UserViewModel user)
        {
            User userEntity = mapper.Map<User>(user);
            return await userRepository.GetUserIdAsync(userEntity);
        }

        public async Task<string> GetUserTokenAsync(UserViewModel user)
        {
            User userEntity = mapper.Map<User>(user);
            return await userRepository.GetUserTokenAsync(userEntity);
        }

        public async Task LogOutAsync()
        {
            await userRepository.LogOutAsync();
        }

        public async Task<bool> SignInUserAsync(UserViewModel user)
        {
            User userEntity = await userRepository.GetByEmailAsync(user.Email);
            SignInResult result = await userRepository.SignInUserAsync(userEntity, user.Password);
            return result.Succeeded ? true : false;
        }

        public async Task<UserViewModel> FindByNameAsync(string name)
        {
            User userEntity = await userRepository.FindByNameAsync(name);
            UserViewModel user = mapper.Map<UserViewModel>(userEntity);
            return user;
        }
    }
}
