using Microsoft.AspNetCore.Identity;

namespace ItNews.Data.Contracts.Entities
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