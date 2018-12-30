using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class PostCodePropertyValidator : ClientValidatorBase, IClientModelValidator
    {
        public PostCodePropertyValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var validator = (PostCodeValidator)Validator;

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-postcode", "Wrong PostCode");
        }
    }
}
