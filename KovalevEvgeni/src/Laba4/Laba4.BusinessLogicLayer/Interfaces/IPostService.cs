using Laba4.BusinessLogicLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostViewModel> GetPostes(string userId);
        PostViewModel GetPost(int postId);

        void Insert(PostViewModel record);
        void Update(PostViewModel record);
        void Delete(int postId);
    }
}
