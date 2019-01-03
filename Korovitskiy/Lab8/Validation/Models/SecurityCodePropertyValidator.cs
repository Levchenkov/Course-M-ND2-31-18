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
    public class SecurityCodePropertyValidator : ClientValidatorBase, IClientModelValidator
    {
        public SecurityCodePropertyValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var validator = (SecurityCodeValidator)Validator;

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-securitycode", "Wrong SecurityCode");
        }
    }
}
