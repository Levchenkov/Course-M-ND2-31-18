using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6Crocodile.Models
{
    public static class GroupManager
    {
        public static string DefaultName { get; set; }
        public static List<string> Groups { get; set; }
        public static Dictionary<string, string> UserAndGroup { get; set; }
        static GroupManager()
        {
            Groups = new List<string>();
            UserAndGroup = new Dictionary<string, string>();
            DefaultName = "Создать";
            Groups.Add(DefaultName);
        }
    }
}
