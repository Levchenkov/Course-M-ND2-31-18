using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab6.Models
{
    public class Group
    {
        public string Id { get; set; }
        public bool StartGame { get; set; }
        public int NumberOfPlayers { get; set; }
        public User Leader { get; set; }
        public List<User> Users { get; set; }
        public string Key { get; set; }

        public Group()
        {
            Users = new List<User>();
            StartGame = false;
            Leader = null;
        }
    }
}