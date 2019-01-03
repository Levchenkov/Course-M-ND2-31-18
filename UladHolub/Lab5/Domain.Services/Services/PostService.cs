using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModel;
using Domain.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PostViewModel> GetLastestRecords(int recordsNumber)
        {
            var posts = unitOfWork.PostRepository.GetLastestRecords(recordsNumber);
            var postsViewModel = DomainMapper.Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            return postsViewModel;
        }

        public async Task<IOperationDetails> CreatePostAsync(PostViewModel postViewModel)
        {
            var post = DomainMapper.Mapper.Map<PostViewModel, Post>(postViewModel);
            var user = await unitOfWork.UserManager.FindByIdAsync(postViewModel.User.Id);
            if(user == null) { return new OperationDetails(true, "User not found", ""); }
            post.Id = Guid.NewGuid().ToString();
            post.User = user;
            post.Date = DateTime.Now;            
            unitOfWork.PostRepository.Create(post);
            await unitOfWork.SaveAsync();
            return new OperationDetails(true, "Post successfully created", "");
        }

        public async Task<IOperationDetails> UpdatePostAsync(PostViewModel postViewModel)
        {
            var post = unitOfWork.PostRepository.Get(postViewModel.Id);
            if (post == null) { return new OperationDetails(true, "Post not found", ""); ; }
            post.Content = postViewModel.Content;
            unitOfWork.PostRepository.Update(post);
            await unitOfWork.SaveAsync();
            return new OperationDetails(true, "Post successfully updated", "");
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
