using Lab4.Data.Contracts.Repositories;
using Lab4.Domain.Contracts;
using Lab4.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PostContract = Lab4.Data.Contracts.Entities.Post;

namespace Lab4.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IContextFactory contextFactory;

        public PostService(IContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public void AddPost(Post post, string userId)
        {
            using (var context = contextFactory.GetContext())
            {
                var user = context.UsersRepository.Where(m => m.Id == userId).FirstOrDefault();
                context.Add(context.PostsRepository, new PostContract()
                {
                    Id = post.Id,
                    Content = post.Content,
                    Created = DateTime.Now,
                    User=user
                });

                context.Save();
            }
        }


        public List<Post> GetPosts(string userId)
        {
            using (var context = contextFactory.GetContext())
            {
                var result = context.PostsRepository.Where(x => x.UserId==userId)
                    .Join(context.UsersRepository, p => p.UserId, x => x.Id, (p, x) => new Post()
                    {
                        Id = p.Id,
                        Content = p.Content,
                        Created = p.Created,
                        User= new User() { Id=userId }
                    }).Take(20).ToList();

                return result;
            }
        }

        public void UpdatePost(Post post)
        {
            using (var context = contextFactory.GetContext())
            {
                var updatePost = context.PostsRepository.FirstOrDefault(m => m.Id == post.Id);

                if (updatePost != null)
                {
                    updatePost.Content = post.Content;
                    updatePost.Created = DateTime.Now;
                    context.Save();
                }
            }
        }

        public Post FindContentPost(int id, string userId)
        {
            using (var context = contextFactory.GetContext())
            {
                var updatePost = context.PostsRepository
                    .Where(x=> x.Id==id && x.UserId==userId).FirstOrDefault();

                if (updatePost != null)
                {
                    var post = new Post()
                    {
                        Id = id,
                        User = new User() { Id = userId },
                        Content = updatePost.Content
                    };
                    return post;
                }
                else
                {
                    return null;
                }
            }
        }

    }



}

