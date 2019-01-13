using AutoMapper;
using Data.Repositories.Entities;
using Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public static class AppMapper
    {
        private static IMapper mapper;

        public static IMapper Mapper
        {
            get
            {
                return mapper;
            }
        }

        static AppMapper()
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserViewModel, User>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                        .ReverseMap()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

                    cfg.CreateMap<TwitViewModel, Twit>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                        .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                        .ReverseMap()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                        .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
                }).CreateMapper();
        }
    }
}
