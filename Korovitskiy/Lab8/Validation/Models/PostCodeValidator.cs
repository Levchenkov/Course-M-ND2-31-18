using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class PostCodeValidator : PropertyValidator
    {
        public PostCodeValidator()
            : base("Wrong PostCode")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            int digit;
            if (!int.TryParse(context.PropertyValue.ToString(), out digit))
            {
                return false;
            }
            if (digit < 10000 && digit > 99999)
            {
                return false;
            }
            return true;
        }
    }
}
