using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public Student Author { get; set; }

        public DateTime Created { get; set; }

        public virtual Post Post { get; set; }
    }
}