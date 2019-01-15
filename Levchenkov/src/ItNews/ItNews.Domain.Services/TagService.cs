using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItNews.Data.Contracts;
using ItNews.Data.Contracts.Entities;
using ItNews.Domain.Contracts;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly IDbScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public TagService(IDbScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public IEnumerable<TagViewModel> GetTags(long postId)
        {
            using (var scope = scopeFactory.GetSharedScope())
            {
                var tagIds = scope.CreateQuery<PostTag>().Where(x => x.PostId == postId).Select(x => x.TagId);
                var tags = scope.CreateQuery<Tag>().Where(x => tagIds.Contains(x.Id));
                var result = mapper.Map<IEnumerable<TagViewModel>>(tags);
                return result;
            }
        }
    }
}