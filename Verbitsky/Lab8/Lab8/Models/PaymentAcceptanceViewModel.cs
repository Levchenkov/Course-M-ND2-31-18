using System;
using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{

    public class PaymentAcceptanceViewModel
    {
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Display(Name = "Post Code")]
        [DataType(DataType.Text)]
        public string PostCode { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Credit Card Number")]
        [DataType(DataType.MultilineText)]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Expiration")]
        [DataType(DataType.Date)]
        public DateTime ExpirationMonthAndYear { get; set; }

        [Display(Name = "Security Code")]
        [DataType(DataType.Text)]
        public string SecurityCode { get; set; }
    }
}
