using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lab4.Data.Contracts.Entities;
using Lab4.Domain.Contracts.ViewModels;

namespace Lab4.Infrastructure
{
    public class AutomapperWebProfile : Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<TweetViewModel, TweetEntity>()
                .ForMember(d => d.TweetId, o => o.MapFrom(s => s.TweetId))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<TweetEntity, TweetViewModel>()
                .ForMember(d => d.TweetId, o => o.MapFrom(s => s.TweetId))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<Task<TweetViewModel>, Task<TweetEntity>>();
            CreateMap<Task<TweetEntity>, Task<TweetViewModel>>();

            CreateMap<PersonViewModel, PersonEntity>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.EmailConfirmed, o => o.MapFrom(s => s.EmailConfirmed))
                .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.PasswordHash))
                .ForMember(d => d.TweetList, o => o.MapFrom(s => s.Tweets))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                .ForAllOtherMembers(o => o.Ignore());
            CreateMap<PersonEntity, PersonViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.EmailConfirmed, o => o.MapFrom(s => s.EmailConfirmed))
                .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.PasswordHash))
                .ForMember(d => d.Tweets, o => o.MapFrom(s => s.TweetList))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                .ForAllOtherMembers(o => o.Ignore());
        }

        public static void Run()
        {
            Mapper.Initialize(c => c.AddProfile<AutomapperWebProfile>());
        }
    }
}
