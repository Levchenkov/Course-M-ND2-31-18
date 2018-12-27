using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Models.ViewModels
{
    public class VMStudent
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}