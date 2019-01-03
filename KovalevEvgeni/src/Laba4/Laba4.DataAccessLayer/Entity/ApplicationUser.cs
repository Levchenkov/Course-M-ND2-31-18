using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullNameUser { get; set; }

    }
}
