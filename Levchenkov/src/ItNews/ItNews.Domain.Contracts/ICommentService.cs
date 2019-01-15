using System.Collections.Generic;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Contracts
{
    public interface ICommentService
    {
        IEnumerable<CommentViewModel> GetComments(long postId);
    }
}