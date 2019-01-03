using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Validation_CardHolder.Models;

namespace Validation_CardHolder.Validations
{
    public class CardHolderViewModelValidator : AbstractValidator<CardHolderViewModel>
    {
        private readonly string _regexAddress = @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$";
        private readonly string _regexCity = @"^[a-zA-Z-\s]+$";
        private readonly string _regexCountry = @"^[a-zA-Z-\s]+$";
        private readonly string _regexPostCode = @"^[0-9]{5}$";
        private readonly string _regexSecurityCode = @"^[0-9]{3,4}$";
        public CardHolderViewModelValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty();
            RuleFor(model => model.MiddleName).NotEmpty();
            RuleFor(model => model.LastName).NotEmpty();
            RuleFor(model => model.Address).NotEmpty().Matches(_regexAddress);
            RuleFor(model => model.City).NotEmpty().Matches(_regexCity);
            RuleFor(model => model.Country).NotEmpty().Matches(_regexCountry);
            RuleFor(model => model.PostCode).NotEmpty().Matches(_regexPostCode);
            RuleFor(model => model.Email).NotEmpty().EmailAddress();
            RuleFor(model => model.Amount).InclusiveBetween((decimal)0.01, (decimal)99999.99).SetValidator(new ScalePrecisionValidator(2, 7));
            RuleFor(model => model.Description).Length(0, 250);
            RuleFor(model => model.CreditCardNumber).CreditCard();
            RuleFor(model => model.ExpirationMonth).InclusiveBetween(1, 12);
            RuleFor(model => model.ExpirationYear).InclusiveBetween(DateTime.Now.Year, 2999);
            When(model => model.ExpirationYear.Equals(DateTime.Now.Year), () =>
                RuleFor(model => model.ExpirationMonth).InclusiveBetween(DateTime.Now.Month,12));
            RuleFor(model => model.SecurityCode).NotEmpty().Matches(_regexSecurityCode);
        }

    }
}
