using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Domain.Contracts.InfoModels
{
    public interface IUserInfo
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string PasswordHash { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        bool RememberMe { get; set; }
    }
}
