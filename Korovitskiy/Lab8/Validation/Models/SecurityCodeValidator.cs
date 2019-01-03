using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class SecurityCodeValidator : PropertyValidator
    {
        public SecurityCodeValidator()
            : base("Wrong SecurityCode")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            int digit;
            if (!int.TryParse(context.PropertyValue.ToString(), out digit))
            {
                return false;
            }
            if (digit < 100 && digit > 999)
            {
                return false;
            }
            return true;
        }
    }
}
