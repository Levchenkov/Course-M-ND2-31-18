using FluentValidation;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Lab8.Models
{
    public class CustomPaymentInfoValidator:AbstractValidator<CustomPaymentInfo>
    {        
        private Regex addressRegex = new Regex("[a-zA-Zа-яА-Я\\d.,/\\-\\s]*$");
        private Regex cityRegex = new Regex("[a-zA-Zа-яА-Я\\s-]*$");
        private Regex postCodeRegex = new Regex("^[0-9]{5}$");
        private Regex ccnRegex = new Regex("^[0-9]{16}$");
        private Regex cvvRegex = new Regex("^[0-9]{3}$");

        public CustomPaymentInfoValidator()
        {   
            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("First name is required");
            RuleFor(c => c.MiddleName)
                .NotNull().WithMessage("Middle name is required");
            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Last name is required");
            
            RuleFor(c => c.Address)
                .NotNull().WithMessage("Address is required")
                .Matches(addressRegex).WithMessage("Address is invalid");
            RuleFor(c => c.City)
                .NotNull().WithMessage("City is required")
                .Matches(cityRegex).WithMessage("City is invalid");
            RuleFor(c => c.Country)
                .NotNull().WithMessage("Country is required")
                .Matches(cityRegex).WithMessage("Country is invalid");
            RuleFor(c => c.PostCode)
                .NotNull().WithMessage("Post code is required")
                .Matches(postCodeRegex).WithMessage("Post code must have 5 digits")
                .Must(code=>isPostCodeValid(code)).WithMessage("Post code must have 5 digits");
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email adress is required")
                .EmailAddress().WithMessage("Your email address is invalid");
            RuleFor(c => c.Amount)
                .NotNull().WithMessage("Amount of payment is required")
                .InclusiveBetween(0.01m,99999.99m).WithMessage("Amount of payment range is from 0.01 to 99999.99");
            
            RuleFor(c => c.Description)
                .Length(0, 250).WithMessage("Description must not be greater than 250 symbols");
            RuleFor(c => c.CCN)
                .Matches(ccnRegex).WithMessage("CCN must have 16 numbers")
                .Must(code=>isCreditCardNumber(code)).WithMessage("CCN is not valid");
            RuleFor(c => c.ExpirationMonth)
                .InclusiveBetween(1, 12).WithMessage("Expiration Month must be from 1 to 12")
                .Must(month=>isMonthMoreThanNow(month)).WithMessage("Month must be more than current one");
            RuleFor(c=>c.ExpirationYear)                
                .Must(year => isYearMoreThanNow(year)).WithMessage("Year must be more than current one");
            RuleFor(c => c.SecurityCode)
                .Matches(cvvRegex).WithMessage("CVV must have 3 numbers");


        }

        private bool isYearMoreThanNow(int? year)
        {
            if (year == null)
            {
                return true;
            }
            return DateTime.Now.Year <= year;
        }

        private bool isMonthMoreThanNow(int? month)
        {
            if (month == null)
            {
                return true;
            }
            return DateTime.Now.Month <= month;
        }

        private bool isCreditCardNumber(string ccnNumber)
        {
            if (ccnNumber == null)
            {
                return true;
            }

            int[] sumNumbers = new int[ccnNumber.Length];
            bool jump = false;
            int sum = 0;
            for(int i=ccnNumber.Length-1; i >= 0; i--)
            {
                sumNumbers[i] = int.Parse(ccnNumber[i].ToString());
                if (!jump)
                {
                    int doubledNumber = sumNumbers[i] *2;
                    if (doubledNumber > 9)
                    {
                        int x = (int)doubledNumber / 10;
                        int y = doubledNumber % 10;
                        sumNumbers[i] = x + y;
                    }
                    else
                    {
                        sumNumbers[i] = doubledNumber;
                    }
                    jump = true;
                }
                else
                {
                    jump = false;
                }
                sum += sumNumbers[i];
            }
            return sum % 10 == 0;
        }

        private bool isPostCodeValid(string postCode)
        {
            return postCode.Length == 5;
        }
    }
}