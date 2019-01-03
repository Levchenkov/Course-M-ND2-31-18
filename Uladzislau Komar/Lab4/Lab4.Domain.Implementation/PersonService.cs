using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Data.Contracts.Repositories;
using Lab4.Domain.Contracts.Services;
using Lab4.Domain.Contracts.ViewModels;
using AutoMapper;
using Lab4.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lab4.Domain.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository repository;
        private readonly IMapper mapper;
        private readonly UserManager<PersonEntity> userManager;
        private readonly SignInManager<PersonEntity> signInManager;
        
        public PersonService(IPersonRepository repository, IMapper mapper, UserManager<PersonEntity> userManager, SignInManager<PersonEntity> signInManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(PersonViewModel model, string callbackUrl)
        {
            var user = new PersonViewModel { Email = model.Email, UserName = model.UserName };
            var userEntity = mapper.Map<PersonViewModel, PersonEntity>(user);
            userEntity.UserName = model.UserName;
            var result = await userManager.CreateAsync(userEntity, model.PasswordHash);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(userEntity, false);
            }
            return result;
        }

        public async Task<SignInResult> Login(PersonViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, false, false);
            return result;
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<PersonViewModel> GetCurrentUserAsync(HttpContext context)
        {
            var entity = await userManager.GetUserAsync(context.User);
            var model = mapper.Map<PersonEntity, PersonViewModel>(entity);
            return model;
        }

        public async Task<string> GetEmailComfirmationCodeAsync(PersonViewModel model)
        {
            var entity = mapper.Map<PersonViewModel, PersonEntity>(model);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(entity);
            return code;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, code);
            return result;
        }
    }
}
