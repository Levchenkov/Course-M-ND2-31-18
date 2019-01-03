using AutoMapper;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.BusinessLogicLayer.Infrastructure;
using Laba4.BusinessLogicLayer.Interfaces;
using Laba4.DataAccessLayer.Entity;
using Laba4.DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork database;
        private IMapper mapper;
        public UserService(IUnitOfWork database)
        {
            this.database = database;
            mapper = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); }).CreateMapper();
        }

        public string UserId { get; set; }

        public bool StatusEmailConfirmed { get; set; }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserViewModel record)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await database.UserManager.FindAsync(record.Email, record.Password);
            if (user != null)
            {
                StatusEmailConfirmed = user.EmailConfirmed;
                claim = await database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                claim.AddClaim(new Claim("FullUserName", user.FullNameUser??""));
            }
            return claim;
        }

        public async Task<bool> EmailConfirmation(string userId,string token)
        {
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("YourAppName");
            database.UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            var result = await database.UserManager.ConfirmEmailAsync(userId, token);
            return result.Succeeded;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("YourAppName");
            database.UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            return await database.UserManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            database.UserManager.EmailService = new EmailService();
            await database.UserManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<OperationDetails> CreateAsync(UserViewModel record)
        {
            ApplicationUser user = await database.UserManager.FindByEmailAsync(record.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = record.Email, UserName = record.Email, FullNameUser = record.UserName };
                var result = await database.UserManager.CreateAsync(user, record.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                UserId = user.Id;
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public Task SetInitialData(UserViewModel adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
