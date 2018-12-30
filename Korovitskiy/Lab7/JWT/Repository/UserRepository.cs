using JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Repository
{
    public class UserRepository
    {
        public static IList<User> Users = new List<User>
        {
            new User {Login="admin", Password="123", Role = "admin" },
            new User { Login="qwerty", Password="55555", Role = "user" }
        };
    }
}
