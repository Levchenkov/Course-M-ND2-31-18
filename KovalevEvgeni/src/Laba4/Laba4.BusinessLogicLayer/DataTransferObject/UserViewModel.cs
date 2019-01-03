using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.DataTransferObject
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Logins { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
