using AutoMapper;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(post => post.Author, map => map.MapFrom(item => item.User.FullNameUser))
                .ForMember(post => post.Content, map => map.MapFrom(x => x.Content))
                .ForMember(post => post.DateCreate, map => map.MapFrom(x => x.DateCreate))
                .ForMember(post => post.PostId, map => map.MapFrom(x => x.PostId))
                .ForMember(post => post.TitlePost, map => map.MapFrom(x => x.TitlePost))
                .ForMember(post => post.UserId, map => map.MapFrom(x => x.UserId));
            CreateMap<PostViewModel, Post>()
               .ForMember(post => post.Content, map => map.MapFrom(x => x.Content))
               .ForMember(post => post.DateCreate, map => map.MapFrom(x => x.DateCreate))
               .ForMember(post => post.PostId, map => map.MapFrom(x => x.PostId))
               .ForMember(post => post.TitlePost, map => map.MapFrom(x => x.TitlePost))
               .ForMember(post => post.UserId, map => map.MapFrom(x => x.UserId));
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(post => post.Email, map => map.MapFrom(item => item.Email))
                .ForMember(post => post.EmailConfirmed, map => map.MapFrom(x => x.EmailConfirmed))
                .ForMember(post => post.Logins, map => map.MapFrom(x => x.Logins))
                .ForMember(post => post.Password, map => map.MapFrom(x => x.PasswordHash))
                .ForMember(post => post.UserId, map => map.MapFrom(x => x.Id))
                .ForMember(post => post.UserName, map => map.MapFrom(x => x.FullNameUser));
            CreateMap<UserViewModel, ApplicationUser>()
               .ForMember(post => post.Email, map => map.MapFrom(item => item.Email))
               .ForMember(post => post.EmailConfirmed, map => map.MapFrom(x => x.EmailConfirmed))
               .ForMember(post => post.Logins, map => map.MapFrom(x => x.Logins))
               .ForMember(post => post.PasswordHash, map => map.MapFrom(x => x.Password))
               .ForMember(post => post.Id, map => map.MapFrom(x => x.UserId))
               .ForMember(post => post.FullNameUser, map => map.MapFrom(x => x.UserName));
        }
    }
}
