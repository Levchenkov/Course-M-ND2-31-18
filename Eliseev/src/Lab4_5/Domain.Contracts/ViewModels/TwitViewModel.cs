using System;

namespace Domain.Contracts.ViewModels
{
    public class TwitViewModel
    {
        public string Id
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public virtual UserViewModel User
        {
            get;
            set;
        }
    }
}
