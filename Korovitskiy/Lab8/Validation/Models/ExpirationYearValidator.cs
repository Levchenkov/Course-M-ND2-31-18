using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class ExpirationYearValidator : PropertyValidator
    {
        public ExpirationYearValidator()
            : base("Wrong Expiration Year")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return (int)context.PropertyValue >= DateTime.Now.Year;
        }
    }
}
