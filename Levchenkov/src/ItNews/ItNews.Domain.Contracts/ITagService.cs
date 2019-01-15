using System.Collections.Generic;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Contracts
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetTags(long postId);
    }
}