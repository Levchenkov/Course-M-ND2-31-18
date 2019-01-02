using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models
{
    public class PaymentValidator: AbstractValidator<PaymentViewModel>
    {
        public PaymentValidator()
        {
            RuleFor(model => model.FirstName).NotNull();
            RuleFor(model => model.MiddleName).NotNull();
            RuleFor(model => model.LastName).NotNull();
            RuleFor(model => model.Address)
                .Matches(@"^[\w\s-,]+$")
                .NotNull();
            RuleFor(model => model.City)
                .Matches(@"^[a-zA-Z -]+$")
                .NotNull();
            RuleFor(model => model.Country)
                .Matches(@"^[a-zA-Z -]+$")
                .NotNull();
            RuleFor(model => model.PostCode).Must(x => (x > 9999 && x < 100000)).NotNull();
            RuleFor(model => model.Email)
                .EmailAddress()
                .NotNull();
            RuleFor(model => model.Amount).Must(x => x > 0.01 && x < 99999.99).NotNull();
            RuleFor(model => model.Description)
                .MaximumLength(250);
            RuleFor(model => model.CreditCardNumber)
                .Must(x => x > 999999999999999 && x < 10000000000000000)
                .Custom((cardNumber, context) => 
                {
                    var cardString = cardNumber.ToString();
                    var sum = 0;
                    for (int i = 0; i < cardString.Length; i++)
                    {
                        var digit = int.Parse(cardString.Substring(i, 1));
                        if (i % 2 != 0)
                        {
                            digit *= 2;
                            if (digit > 9)
                            {
                                digit = (digit % 10) + 1;
                            }
                        }
                        sum += digit;
                    }
                    if (sum % 10 != 0)
                    {
                        context.AddFailure("Not valid card number.");
                    }
                });
            RuleFor(model => model.ExpirationYear).Must(x => x > DateTime.Now.Year);
            RuleFor(model => model.ExpirationMonth)
                .Must(x => x > 0 && x < 13)
                .Must((model, month, context) =>
                {
                    var now = DateTime.Now.Date;
                    var inputDate = new DateTime(model.ExpirationYear, month, now.Day);
                    return inputDate >= now;
                });
            RuleFor(model => model.SecurityCode).Must(x => x > 99 && x < 1000);
        }
    }
}
