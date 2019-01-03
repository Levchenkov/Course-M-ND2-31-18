using Lab4.Domain.Contracts.Entities;
using System.Collections.Generic;

namespace Lab4.Domain.Contracts
{
    public interface IPostService
    {
        void AddPost(Post post, string userId);
        List<Post> GetPosts(string userId);
        void UpdatePost(Post post);
        Post FindContentPost(int id, string userId);
    }
}
