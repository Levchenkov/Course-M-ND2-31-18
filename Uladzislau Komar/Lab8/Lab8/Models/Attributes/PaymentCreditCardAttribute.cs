using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models.Attributes
{
    public class PaymentCreditCardAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (PaymentViewModel)validationContext.ObjectInstance;
            var cardString = model.CreditCardNumber.ToString();
            var sum = 0;
            for (int i = 0; i < cardString.Length; i++)
            {
                var digit = int.Parse(cardString.Substring(i, 1));
                if (i % 2 != 0)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit = (digit % 10) + 1;
                    }
                }
                sum += digit;
            }
            if (sum % 10 != 0)
            {
                return new ValidationResult("Not valid credit card number.");
            }
            return ValidationResult.Success;
        }
    }
}
