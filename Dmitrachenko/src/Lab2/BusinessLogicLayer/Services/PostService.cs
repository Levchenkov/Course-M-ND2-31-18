using AutoMapper;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }

        public PostService(IUnitOfWork iOfWork, IMapper mapper)
        {
            UnitOfWork = iOfWork;
            Mapper = mapper;
        }
        public IEnumerable<PostDataModel> GetAllPosts()
        {
            var postDataModels = Mapper.Map<IEnumerable<Post>, IEnumerable<PostDataModel>>(UnitOfWork.Posts.GetAll());
            return postDataModels;
        }
        public void Create(CreatePostDataModel createPostDataModel)
        {
            var tags = createPostDataModel.Tags.Split(new[] { ',' }, options: StringSplitOptions.RemoveEmptyEntries);
            var newPostDataModel = new PostDataModel
            {
                AuthorId = createPostDataModel.AuthorId,
                Created = DateTime.Now,
                Content = createPostDataModel.Content,
                Tags = new List<TagDataModel>()
            };
            foreach (var tag in tags)
            {
                newPostDataModel.Tags.Add(new TagDataModel { Name = tag.Trim() });
            }
            var newPost = Mapper.Map<PostDataModel, Post>(source: newPostDataModel);
            UnitOfWork.Posts.Create(newPost);
            UnitOfWork.Save();
        }
        public PostDataModel Get(int id)
        {
            var post = UnitOfWork.Posts.Get(id);
            var postDataModel = Mapper.Map<Post, PostDataModel>(post);
            return postDataModel;
        }
        public CreatePostDataModel Get(PostDataModel postDataModel)
        {
            var listTags = postDataModel.Tags.Select(model => model.Name);
            var tagsString = string.Join(",", listTags);
            var createPostViewModel = Mapper.Map<PostDataModel, CreatePostDataModel>(postDataModel);
            createPostViewModel.Tags = tagsString;
            return createPostViewModel;
        }
        public void Edit(CreatePostDataModel createPostDataModel)
        {
            var editPost = UnitOfWork.Posts.Get(createPostDataModel.Id);
            editPost.Content = createPostDataModel.Content;
            var tags = createPostDataModel.Tags.Split(new[] { ',' }, options: StringSplitOptions.RemoveEmptyEntries);
            foreach (var tag in tags)
            {
                if (editPost.Tags.Any(t => t.Name == tag.Trim())) continue;
                var newTag = new Tag() { Name = tag.Trim() };
                UnitOfWork.Tags.Create(newTag);
                editPost.Tags.Add(newTag);
            }
            UnitOfWork.Posts.Update(editPost);
            UnitOfWork.Save();
        }
        public void Delete(int id)
        {
            UnitOfWork.Posts.Delete(id);
            UnitOfWork.Save();
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
