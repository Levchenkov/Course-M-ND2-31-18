using Lab8.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models
{
    public class PaymentViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[\w\s-,]+$")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z -]+$")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z -]+$")]
        public string Country { get; set; }
        [Required]
        [Range(10000, 99999)]
        public int PostCode { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(0.01, 99999.99)]
        public double Amount { get; set; }
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Range(1000000000000000, 9999999999999999)]
        [PaymentCreditCard]
        public ulong CreditCardNumber { get; set; }
        [Range(1,12)]
        [ExpirationDate]
        public int ExpirationMonth { get; set; }
        [Range(1970, 9999)]
        [ExpirationDate]
        public int ExpirationYear { get; set; }
        [Range(100, 999)]
        public int SecurityCode { get; set; }
    }
}
