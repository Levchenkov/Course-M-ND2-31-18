using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItNews.Data.Contracts;
using ItNews.Data.Contracts.Entities;
using ItNews.Domain.Contracts;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDbScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public CommentService(IDbScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public IEnumerable<CommentViewModel> GetComments(long postId)
        {
            using (var scope = scopeFactory.GetSharedScope())
            {
                var comments = scope.CreateQuery<Comment>().Where(x => x.PostId == postId);

                var result = mapper.Map<IEnumerable<CommentViewModel>>(comments);

                return result;
            }
        }
    }
}