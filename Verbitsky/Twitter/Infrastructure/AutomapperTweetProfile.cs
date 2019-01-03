using AutoMapper;
using Data.Contracts.Models;
using DomainContracts.Models.ViewModel;

namespace Web.Infrastructure
{
    public class AutomapperTweetProfile : Profile
    {
        public AutomapperTweetProfile()
        {
            CreateMap<TweetViewModel, TweetEntity>()
                .ForPath(a => a.Id, a => a.MapFrom(b => b.Id))
                .ForPath(a => a.Head, a => a.MapFrom(b => b.Head))
                .ForPath(a => a.Content, a => a.MapFrom(b => b.Content))
                .ForAllOtherMembers(a => a.Ignore());

            CreateMap<TweetEntity, TweetViewModel>()
                .ForPath(a => a.Id, a => a.MapFrom(b => b.Id))
                .ForPath(a => a.AuthorId, a => a.MapFrom(b => b.Author.Id))
                .ForPath(a => a.Author, a => a.MapFrom(b => b.Author.FirstName))
                .ForPath(a => a.Head, a => a.MapFrom(b => b.Head))
                .ForPath(a => a.Content, a => a.MapFrom(b => b.Content))
                .ForAllOtherMembers(a => a.Ignore());
        }
    }
}
