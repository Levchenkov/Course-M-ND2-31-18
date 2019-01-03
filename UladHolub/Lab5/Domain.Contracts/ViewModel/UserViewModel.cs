using System.Collections.Generic;

namespace Domain.Contracts.ViewModel
{
    public class UserViewModel
    {
        public virtual string Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string UserName { get; set; }
    }
}
