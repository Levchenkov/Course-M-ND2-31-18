using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Models.ViewModels
{
    public class VMComment
    {
        public string Content { get; set; }

        public Student Author { get; set; }

        public DateTime Created { get; set; }

        public Post Post { get; set; }
    }
}