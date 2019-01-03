using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Switter.Web.Crocodile.Models
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool Master { get; set; }
        public string Word { get; set; }
    }
}
