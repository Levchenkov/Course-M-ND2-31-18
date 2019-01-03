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
            RuleFor(payment => payment.PostCode).Must(x => x > 9999 && x < 100000);
            RuleFor(payment => payment.Email).NotNull().EmailAddress();
            RuleFor(payment => payment.Amount).Must(x => x > 0.01 && x < 99999.99);
            RuleFor(payment => payment.Description).Null().MaximumLength(250);
            RuleFor(payment => payment.CreditCardNumber)
                .Must(x => x > 999999999999999 && x < 10000000000000000)
                .Custom((digit, context) =>
                  {
                      int[] arrayInt = new int[16];
                      for (int i = 15; i >= 0; i--)
                      {
                          arrayInt[i] = (int)(digit % 10);
                          digit = digit / 10;
                          if (i % 2 != 1)
                          {
                              int digitX2 = arrayInt[i] * 2;
                              arrayInt[i] = digitX2 > 9 ? digitX2 - 9 : digitX2;
                          }
                      }
                      if (arrayInt.Sum() % 10 != 0)
                          context.AddFailure("Wrong number!");
                  });
            RuleFor(payment => payment.ExpirationMonth).Must((payment, month, context) =>
            {
                var now = DateTime.Now.Date;
                var inputDate = new DateTime(payment.ExpirationYear, month, now.Day);
                return inputDate >= now;
            }).WithMessage("Input date is wrong");
            RuleFor(payment => payment.ExpirationYear).Must(x => x >= DateTime.Now.Year);
            RuleFor(payment => payment.SecurityCode).Must(x => x > 99 && x < 1000);
        }
    }
}
