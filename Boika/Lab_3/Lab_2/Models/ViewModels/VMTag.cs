using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Models.ViewModels
{
    public class VMTag
    {
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}