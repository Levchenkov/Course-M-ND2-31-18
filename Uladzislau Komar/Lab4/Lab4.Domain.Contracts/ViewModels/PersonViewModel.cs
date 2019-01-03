using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.Domain.Contracts.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}
