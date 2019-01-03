using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class AmountValidator : PropertyValidator
    {
        public AmountValidator()
            : base("Wrong Amount")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            float digit;
            if (!float.TryParse(context.PropertyValue.ToString(), out digit))
            {
                return false;
            }
            if (digit < 0.01 && digit > 99999.99)
            {
                return false;
            }
            return true;
        }
    }
}
