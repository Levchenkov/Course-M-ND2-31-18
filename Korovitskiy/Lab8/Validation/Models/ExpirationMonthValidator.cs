using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class ExpirationMonthValidator : PropertyValidator
    {
        public ExpirationMonthValidator()
               : base("Input date is wrong")
        {

        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var month = (int)context.PropertyValue;
            if (month < 1 || month > 12)
            {
                return false;
            }
            var payment = (Payment)context.Instance;
            var now = DateTime.Now.Date;
            var inputDate = new DateTime(payment.ExpirationYear, month, now.Day);
            return inputDate >= now;
        }
    }
}
