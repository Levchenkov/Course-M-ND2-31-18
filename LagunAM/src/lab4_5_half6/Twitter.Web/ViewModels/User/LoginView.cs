using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Twitter.Web.ViewModels.User
{
    public class LoginView
    {
        [Required]         
        [EmailAddress(ErrorMessage = "Not Valid Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
	}
}