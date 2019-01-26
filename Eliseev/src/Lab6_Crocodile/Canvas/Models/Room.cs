using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canvas.Models
{
    public class Room
    {
        private List<Player> players;

        public Room()
        {
            players = new List<Player>();
        }

        public string Id
        {
            get;
            set;
        }

        public string Task
        {
            get;
            set;
        }

        public List<Player> Players
        {
            get
            {
                return players;
            }            
        }
    }
}