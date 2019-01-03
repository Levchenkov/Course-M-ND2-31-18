using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models.Attributes
{
    public class ExpirationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (PaymentViewModel)validationContext.ObjectInstance;
            var now = DateTime.Now.Date;
            var inputDate = new DateTime(model.ExpirationYear, model.ExpirationMonth, now.Day);
            if (inputDate >= now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Expiration year must be more than the current date");
        }
    }
}
