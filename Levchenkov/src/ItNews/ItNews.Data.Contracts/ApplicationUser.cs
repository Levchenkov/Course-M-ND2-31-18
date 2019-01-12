using Microsoft.AspNetCore.Identity;

namespace ItNews.Data.Contracts
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked
        {
            get;
            set;
        }
    }
}