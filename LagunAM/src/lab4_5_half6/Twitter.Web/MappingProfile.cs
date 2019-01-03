using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitter.Data.Contracts.Entities;
using Twitter.Domain.Contracts.InfoModels;
using Twitter.Web.ViewModels.User;

namespace Twitter.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<RegistrationView, IUserInfo>();
            CreateMap<IUserInfo, RegistrationView>();
            CreateMap<IUserInfo, ApplicationUser>();
            CreateMap<LoginView, IUserInfo>();
            CreateMap<ITwitInfo, Twit>();
            CreateMap<Twit, ITwitInfo>();
                
        }
    }
}