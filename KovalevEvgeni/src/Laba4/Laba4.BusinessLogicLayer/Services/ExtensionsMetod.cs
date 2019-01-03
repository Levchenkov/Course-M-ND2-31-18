using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Services
{
    public static class ExtensionsMetod
    {
        public static string GetFullUserName(this IIdentity identity)
        {
            ClaimsIdentity userClaim = identity as ClaimsIdentity;
            if (userClaim == null) return "";
            Claim claim= userClaim.FindFirst("FullUserName");
            return claim?.Value ?? "";
        }
    }
}
