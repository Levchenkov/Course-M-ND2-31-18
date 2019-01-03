using Domain.Contracts.ViewModel;
using System.Collections.Generic;

namespace Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public PostViewModel FormPost { get; set; }

        public HomeIndexViewModel()
        {
            Posts = null;
            FormPost = null;
        }
    }
}