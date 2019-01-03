using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab6.Models
{
    public static class Database
    {
        public static string[] Keys { get; set; }
        public static List<User> Users { get; set; }
        public static List<Group> Groups { get; set; }

        static Database()
        {
            Users = new List<User>();
            Groups = new List<Group>();
            Keys = new string[] { "Potato", "Airplane", "House", "Tree", "David Hasselhoff" };
        }
    }
}