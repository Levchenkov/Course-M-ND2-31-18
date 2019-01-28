using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab8.Models
{
    [FluentValidation.Attributes.Validator(typeof(CustomPaymentInfoValidator))]
    public class CustomPaymentInfo
    {
        [Display(Name = "First Name")]
        public string FirstName
        {
            get;
            set;
        }

        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get;
            set;
        }

        [Display(Name = "Address")]
        public string Address
        {
            get;
            set;
        }

        [Display(Name ="City")]
        public string City
        {
            get;
            set;
        }

        [Display(Name ="Country")]
        public string Country
        {
            get;
            set;
        }

        [Display(Name = "Amount of payment")]
        public decimal? Amount
        {
            get;
            set;
        }

        [Display(Name = "Post Code")]
        public string PostCode
        {
            get;
            set;
        }

        [Display(Name = "Email Address")]
        public string Email
        {
            get;
            set;
        }

        [Display(Name ="Description")]
        public string Description
        {
            get;
            set;
        }
        [Display(Name ="Credit Card Number")]
        public string CCN
        {
            get;
            set;
        }

        [Display(Name = "Expiration Month")]
        public int? ExpirationMonth
        {
            get;
            set;
        }

        [Display(Name = "Expiration Year")]
        public int? ExpirationYear
        {
            get;
            set;
        }

        [Display(Name = "Security Code")]
        public string SecurityCode
        {
            get;
            set;
        }

    }
}