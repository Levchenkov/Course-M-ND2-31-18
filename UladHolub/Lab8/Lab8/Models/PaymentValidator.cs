using FluentValidation;
using System;

namespace Lab8.Models
{
    public class PaymentValidator : AbstractValidator<PaymentViewModel>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please enter your first name")
                .MinimumLength(2).WithMessage("Your first name must consist of at least 2 characters");
            RuleFor(x => x.MiddleName)
                .NotEmpty().WithMessage("Please enter your middle name")
                .MinimumLength(2).WithMessage("Your middle name must consist of at least 2 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Please enter your last name")
                .MinimumLength(2).WithMessage("Your last name must consist of at least 2 characters");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Please enter your address")
                .Matches(@"[\w\d\s,\.\-\\\/]+").WithMessage("The address has invalid characters");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Please enter your city")
                .Matches(@"[A-Za-z\s\-]+").WithMessage("The city has invalid characters");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Please enter your country")
                .Matches(@"[A-Za-z\s\-]+").WithMessage("The country has invalid characters");
            RuleFor(x => x.PostCode)
                .NotEmpty().WithMessage("Please enter your post code")
                .Matches(@"[\d]{6}").WithMessage("Post code represents 6-digit code like 111111");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please enter your email")
                .EmailAddress().WithMessage("Enter valid email like user@example.com");
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Please enter amount. Amount must be greater than 0.01 and must be less than 99999.99")
                .GreaterThan(0.01f).WithMessage("Amount must be greater than 0.01 and must be less than 99999.99")
                .LessThan(99999.99f).WithMessage("Amount must be greater than 0.01 and must be less than 99999.99");
            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description max length is 250 symbols");
            RuleFor(x => x.CreditCardNumber)
                .NotEmpty().WithMessage("Please enter credit card number")
                .Matches(@"\d{16}").WithMessage("Credit card number represents 16-digit number like 1234123412341234")
                .Must(CreditCardNumberValidate).WithMessage("Incorrect credit card number");
            RuleFor(x => x.ExpirationMonth)
                .NotEmpty().WithMessage("Please enter expiration month")
                .GreaterThan(0).WithMessage("Please enter valid expiration month")
                .LessThan(13).WithMessage("Please enter valid expiration month")
                .Must((x,y) => DataValidate(x)).WithMessage("Date must be greater than current");
            RuleFor(x => x.ExpirationYear)
                .NotEmpty().WithMessage("Please enter expiration year")
                .Must((x, y) => DataValidate(x)).WithMessage("Date must be greater than current"); ;
            RuleFor(x => x.SecurityCode)
                .NotEmpty().WithMessage("Please enter security code")
                .Matches(@"[\d]{3}").WithMessage("Security code represents 3-digit code like 123");
        }

        private bool CreditCardNumberValidate(string value)
        {
            if (value.Length != 16) { return false; }
            var sum = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (i % 2 == 0) { sum += ((int)Char.GetNumericValue(value[i]) * 2) % 9; }
                else { sum += (int)Char.GetNumericValue(value[i]); }
            }
            return sum % 10 == 0;
        }

        private bool DataValidate(PaymentViewModel value)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year % 2000;

            if(value.ExpirationYear > currentYear 
                || (value.ExpirationYear == currentYear && value.ExpirationMonth >= currentMonth))
            { return true; }
            else { return false; }
        }
    }
}