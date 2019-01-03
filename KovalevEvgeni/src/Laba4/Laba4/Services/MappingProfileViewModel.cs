using AutoMapper;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laba4.Services
{
    public class MappingProfileViewModel : Profile
    {
        public MappingProfileViewModel()
        {
            CreateMap<RegisterModel, UserViewModel>()
                .ForMember(post => post.Email, map => map.MapFrom(item => item.Email))
                .ForMember(post => post.Password, map => map.MapFrom(x => x.Password))
                .ForMember(post => post.UserName, map => map.MapFrom(x => x.UserName));
            CreateMap<PostModel, PostViewModel>()
                .ForMember(post => post.Content, map => map.MapFrom(item => item.Content))
                .ForMember(post => post.TitlePost, map => map.MapFrom(item => item.TitlePost));
                
        }
    }
}