using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.FirstName).NotNull();
            RuleFor(payment => payment.MidleName).NotNull();
            RuleFor(payment => payment.LastName).NotNull();
            RuleFor(payment => payment.Address).NotNull().Matches(@"[\w',-\\/.\s]+");
            RuleFor(payment => payment.City).NotNull().Matches(@"[a-zA-Z -]+");
            RuleFor(payment => payment.Country).NotNull().Matches(@"[a-zA-Z -]+");
            RuleFor(payment => payment.PostCode).SetValidator(new PostCodeValidator());
            RuleFor(payment => payment.Email).NotNull().EmailAddress();
            RuleFor(payment => payment.Amount).SetValidator(new AmountValidator());
            RuleFor(payment => payment.Description).Null().MaximumLength(250);
            RuleFor(payment => payment.CreditCardNumber).SetValidator(new CreditCardValidator());
            RuleFor(payment => payment.ExpirationMonth).SetValidator(new ExpirationMonthValidator());
            RuleFor(payment => payment.ExpirationYear).SetValidator(new ExpirationYearValidator());
            RuleFor(payment => payment.SecurityCode).SetValidator(new SecurityCodeValidator());
        }
    }
}
