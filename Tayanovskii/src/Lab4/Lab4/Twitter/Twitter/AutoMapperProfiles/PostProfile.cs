using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Twitter.Data.Entities;
using Twitter.Models;

namespace Twitter.AutoMapperProfiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Author, expression => expression.MapFrom(source => source.Author))
                .ForMember(dest => dest.AuthorId, expression => expression.MapFrom(source => source.AuthorId))
                .ForMember(dest => dest.Content, expression => expression.MapFrom(source => source.Content))
                .ForMember(dest => dest.CreatedTime, expression => expression.MapFrom(source => source.CreatedTime))
                .ForMember(dest => dest.Id, expression => expression.MapFrom(source => source.Id))
                .ReverseMap();

        }
    }
}
