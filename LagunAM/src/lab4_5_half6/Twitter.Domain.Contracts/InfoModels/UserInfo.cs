using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Domain.Contracts.InfoModels
{
    public class UserInfo: IUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
    }
}
