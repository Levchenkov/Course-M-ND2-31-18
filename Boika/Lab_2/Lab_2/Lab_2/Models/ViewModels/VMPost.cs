using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Models.ViewModels
{
    public class VMPost
    {
        public string Content { get; set; }

        public Student Author { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}