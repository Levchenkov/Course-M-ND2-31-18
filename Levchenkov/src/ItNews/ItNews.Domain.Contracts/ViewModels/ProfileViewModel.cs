using System.Collections.Generic;

namespace ItNews.Domain.Contracts.ViewModels
{
    public class ProfileViewModel
    {
        public IEnumerable<PostTableViewModel> Posts
        {
            get;
            set;
        }
    }
}