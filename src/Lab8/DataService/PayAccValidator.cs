using FluentValidation;
using Lab8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.DataService
{
    public class PayAccValidator:AbstractValidator<PayAcc>
    {
        public PayAccValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.MiddleName).NotNull();
            RuleFor(x => x.Address).NotNull().Matches(@"[.,\-0-9A-Za-zА-Яа-я\s]+");
            RuleFor(x => x.City).NotNull().Matches(@"[\-0-9A-Za-zА-Яа-я\s]+");
            RuleFor(x => x.Country).NotNull().Matches(@"[\-0-9A-Za-zА-Яа-я\s]+");
            RuleFor(x => x.PostCode).NotNull().Matches(@"[0-9]{0,5}").Length(5,5); 
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Description).Null().MaximumLength(250);
            RuleFor(x => x.SecurityCode).NotNull().Matches(@"[0-9]+").Length(3, 3);
            RuleFor(x => x.ExpirationYear).NotNull().Matches(@"[0-9]+").Length(4, 4);
            RuleFor(x => x.CreditCardNumber).NotNull().Matches(@"[0-9]{0,16}").Length(16, 16);

            






        }
    }
}