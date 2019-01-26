using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canvas.Models
{
    public class Player
    {
        public string ConnectionId
        {
            get;
            set;
        }

        public string RoomId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
    }
}