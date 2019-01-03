using System;

namespace Domain.Contracts.ViewModel
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}
