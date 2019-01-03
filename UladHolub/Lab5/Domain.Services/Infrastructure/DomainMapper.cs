using AutoMapper;
using Data.Contracts.Entities;
using Domain.Contracts.ViewModel;

namespace Domain.Services.Infrastructure
{
    public static class DomainMapper
    {
        private static IMapper mapper;
        public static IMapper Mapper { get { return mapper; } }

        static DomainMapper()
        {
            mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<UserViewModel, User>()
                       .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                       .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.UserName))
                       .ReverseMap()
                       .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                       .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.UserName));
                   cfg.CreateMap<PostViewModel, Post>()
                       .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(d => d.Content, opt => opt.MapFrom(src => src.Content))
                       .ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date))
                       .ForMember(d => d.User, opt => opt.MapFrom(src => src.User))
                       .ReverseMap()
                       .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(d => d.Content, opt => opt.MapFrom(src => src.Content))
                       .ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date))
                       .ForMember(d => d.User, opt => opt.MapFrom(src => src.User));
               }).CreateMapper();
        }
    }
}
