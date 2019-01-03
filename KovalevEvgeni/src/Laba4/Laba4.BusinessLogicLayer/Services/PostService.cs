using AutoMapper;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.BusinessLogicLayer.Interfaces;
using Laba4.DataAccessLayer.Entity;
using Laba4.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork database;
        private IMapper mapper;
        public PostService(IUnitOfWork database)
        {
            this.database = database;
            mapper= new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); }).CreateMapper();
        }
        public void Delete(int postId)
        {
            database.PostRepository.Delete(postId);
            database.Save();
        }

        public PostViewModel GetPost(int postId)
        {
            return mapper.Map<Post, PostViewModel>(database.PostRepository.GetRecord(postId));
        }

        public IEnumerable<PostViewModel> GetPostes(string userId)
        {
            IEnumerable<PostViewModel> listPost = mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(database.PostRepository.GetAll().OrderByDescending(s=>s.DateCreate).Take(20));
            foreach (PostViewModel item in listPost)
            {
                if (item.UserId == userId)
                {
                    item.OperationRecord = true;
                }
            }
            return listPost;
        }

        public void Insert(PostViewModel record)
        {
            record.DateCreate = DateTime.Now;
            Post post = mapper.Map<PostViewModel, Post>(record);
            database.PostRepository.Create(post);
            database.Save();
        }

        public void Update(PostViewModel record)
        {
            Post postEdit = mapper.Map<PostViewModel, Post>(record);
            database.PostRepository.Update(postEdit);
            database.Save();
        }
    }
}
