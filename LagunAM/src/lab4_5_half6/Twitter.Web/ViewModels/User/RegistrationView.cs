using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Twitter.Web.ViewModels.User
{
    public class RegistrationView
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required]
        [DisplayName("User Name")]
        [Remote("CheckUserName", "User", ErrorMessage = "Nick Name In Use")]
        public string UserName { get; set; }

        [DisplayName("PhoneNumber")]
        public string PhoneNumber {get; set;}

        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Not Valid Email")]
        [Remote("CheckEmail", "User", ErrorMessage = "Email In Use")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password is  Differ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
    }
}
