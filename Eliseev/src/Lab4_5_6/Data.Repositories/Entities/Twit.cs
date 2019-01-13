using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Repositories.Entities
{
    public class Twit
    {
        
        public string Id
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public virtual User User
        {
            get;
            set;
        }
    }
}
