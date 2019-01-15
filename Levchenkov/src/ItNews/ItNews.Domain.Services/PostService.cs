using System;
using System.Linq;
using AutoMapper;
using ItNews.Common.Contracts;
using ItNews.Data.Contracts;
using ItNews.Data.Contracts.Entities;
using ItNews.Domain.Contracts;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IDbScopeFactory scopeFactory;
        private readonly IMapper mapper;

        private readonly ICommentService commentService;
        private readonly IRatingService ratingService;
        private readonly ITagService tagService;
        private readonly IProfileService profileService;

        public PostService(
            IDbScopeFactory scopeFactory, 
            IMapper mapper, 
            ICommentService commentService, 
            IRatingService ratingService, 
            ITagService tagService, 
            IProfileService profileService)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
            this.commentService = commentService;
            this.ratingService = ratingService;
            this.tagService = tagService;
            this.profileService = profileService;
        }

        public PostViewModel GetPost(long postId)
        {
            using (var scope = scopeFactory.GetSharedScope())
            {
                var post = scope.CreateQuery<Post>().FirstOrDefault(x => x.Id == postId);

                if (post == null)
                {
                    throw new BusinessException("Post not found.");
                }

                var viewModel = mapper.Map<PostViewModel>(post);
                viewModel = ActualizeViewModel(viewModel);

                return viewModel;
            }
        }

        private PostViewModel ActualizeViewModel(PostViewModel viewModel)
        {
            viewModel.Comments = commentService.GetComments(viewModel.Id);
            viewModel.Author = profileService.GetShortProfile(viewModel.AuthorProfileId);
            viewModel.Rating = ratingService.GetRating(viewModel.Id);
            viewModel.Tags = tagService.GetTags(viewModel.Id);
            return viewModel;
        }

    }
}
