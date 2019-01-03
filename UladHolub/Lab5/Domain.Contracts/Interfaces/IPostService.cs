using Domain.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IPostService : IDisposable
    {
        IEnumerable<PostViewModel> GetLastestRecords(int recordsNumber);
        Task<IOperationDetails> CreatePostAsync(PostViewModel postViewModel);
        Task<IOperationDetails> UpdatePostAsync(PostViewModel postViewModel);
    }
}
