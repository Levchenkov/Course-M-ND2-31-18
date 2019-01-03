using BusinessLogicLayer.DataModel;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDataModel> GetAllPosts();
        void Create(CreatePostDataModel createPostDataModel);
        PostDataModel Get(int id);
        CreatePostDataModel Get(PostDataModel postDataModel);
        void Edit(CreatePostDataModel createPostDataModel);
        void Delete(int id);
        void Dispose();
    }
}
