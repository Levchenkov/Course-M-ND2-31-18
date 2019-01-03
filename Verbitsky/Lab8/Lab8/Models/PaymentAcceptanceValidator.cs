using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace Lab8.Models
{
    public class PaymentAcceptanceValidator : AbstractValidator<PaymentAcceptanceViewModel>
    {
        private static readonly string AddressPattern = @"[A-Za-z0-9А-Яа-я ,.-]+";
        private static readonly string cityPattern = "[A-Za-zА-Яа-я -]+";
        private static readonly string CountryPattern = cityPattern;
        private static readonly string PostCodePattern = @"\d{5}";
        private static readonly string CreditCardNumberPattern = @"\d{16}";
        private static readonly string SecurityCodePattern = @"\d{3}";
        public PaymentAcceptanceValidator()
        {
            RuleFor(a => a.FirstName)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty");
            RuleFor(a => a.MiddleName)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty");
            RuleFor(a => a.LastName)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty");
            RuleFor(a => a.Address)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(AddressPattern));
            RuleFor(a => a.City)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(cityPattern));
            RuleFor(a => a.Country)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(CountryPattern));
            RuleFor(a => a.PostCode)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(PostCodePattern))
                .WithMessage("{PropertyName} must consist of 5 numbers");
            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .EmailAddress();
            RuleFor(a => a.Amount).NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .ExclusiveBetween(0.01m, 99999.99m)
                .WithMessage("{PropertyName} must have a value between 0.01 and 99999.99");
            RuleFor(a => a.Description)
                .Length(0, 250)
                .WithMessage("{PropertyName} must be between 0 and 250");
            RuleFor(a => a.CreditCardNumber)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(CreditCardNumberPattern))
                .WithMessage("{PropertyName} must consist of 16 numbers")
                .Must(cardNo =>  {
                    int nDigits = cardNo.Length;
                    int nSum = 0;
                    bool isSecond = false;
                    for (int i = nDigits - 1; i >= 0; i--)
                    {
                        int d = cardNo[i] - '0';
                        if (isSecond == true)
                            d = d * 2;
                        nSum += d / 10;
                        nSum += d % 10;
                        isSecond = !isSecond;
                    }
                    return (nSum % 10 == 0);
                })
                .WithMessage("{PropertyName} wrong so does not conform to the Luhn formula");
            RuleFor(a => a.ExpirationMonthAndYear)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Must(a => a - DateTime.Now > new TimeSpan(1, 0, 0, 0))
                .WithMessage("{PropertyName} must be greater than the current date");
            RuleFor(a => a.SecurityCode)
                .NotNull()
                .WithMessage("{PropertyName} can not be empty")
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .Matches(new Regex(SecurityCodePattern))
                .WithMessage("{PropertyName} must consist of 3 numbers");
        }
    }
}