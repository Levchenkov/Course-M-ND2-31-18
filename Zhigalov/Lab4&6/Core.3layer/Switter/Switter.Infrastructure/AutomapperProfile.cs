using AutoMapper;
using Switter.Data.Contracts.Entities;
using Switter.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(x => x.Id, s => s.MapFrom(c => c.Id))
                .ForMember(x => x.UserName, s => s.MapFrom(c => c.UserName))
                .ForMember(x => x.Email, s => s.MapFrom(c => c.Email))
                .ForMember(x => x.EmailConfirmed, s => s.MapFrom(c => c.EmailConfirmed))
                .ForMember(x => x.Tweets, s => s.MapFrom(c => c.Tweets))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Id, s => s.MapFrom(c => c.Id))
                .ForMember(x => x.UserName, s => s.MapFrom(c => c.UserName))
                .ForMember(x => x.Email, s => s.MapFrom(c => c.Email))
                .ForMember(x => x.EmailConfirmed, s => s.MapFrom(c => c.EmailConfirmed))
                .ForMember(x => x.Tweets, s => s.MapFrom(c => c.Tweets))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<TweetViewModel, Tweet>()
                .ForMember(x => x.Id, s => s.MapFrom(c => c.Id))
                .ForMember(x => x.Content, s => s.MapFrom(c => c.Content))
                .ForMember(x => x.Date, s => s.MapFrom(c => c.Date))
                .ForMember(x => x.Author, s => s.MapFrom(c => c.Author))
                .ForMember(x => x.AuthorId, s => s.MapFrom(c => c.AuthorId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Tweet, TweetViewModel>()
                .ForMember(x => x.Id, s => s.MapFrom(c => c.Id))
                .ForMember(x => x.Content, s => s.MapFrom(c => c.Content))
                .ForMember(x => x.Date, s => s.MapFrom(c => c.Date))
                .ForMember(x => x.Author, s => s.MapFrom(c => c.Author))
                .ForMember(x => x.AuthorId, s => s.MapFrom(c => c.AuthorId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        public static void Run()
        {
            Mapper.Initialize(x => x.AddProfile<AutomapperProfile>());
        }
    }
}
