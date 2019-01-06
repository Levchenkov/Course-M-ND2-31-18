using FluentValidation;
using FluentValidation.Results;
using Lab8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.DataService
{
    public class PayAccValidator : AbstractValidator<PayAcc>
    {
        public PayAccValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Клиентская: Имя обязательно");
            RuleFor(x => x.LastName).NotNull().WithMessage("Клиентская: Фамилия обязательно");
            RuleFor(x => x.MiddleName).NotNull().WithMessage("Клиентская: Отчество обязательно");
            RuleFor(x => x.Address).NotNull().WithMessage("Клиентская: Адрес обязательно")
                .Matches(@"[.,\-0-9A-Za-zА-Яа-я\s]+").WithMessage("Клиентская: Только разрешенные символы!");
            RuleFor(x => x.City).NotNull().WithMessage("Клиентская: Город обязательно")
                .Matches(@"[\-0-9A-Za-zА-Яа-я\s]+").WithMessage("Клиентская: Только разрешенные символы!");
            RuleFor(x => x.Country).NotNull().WithMessage("Клиентская: Страна обязательно")
                .Matches(@"[\-0-9A-Za-zА-Яа-я\s]+").WithMessage("Клиентская: Только разрешенные символы!");
            RuleFor(x => x.PostCode).NotNull().WithMessage("Клиентская: Почтовый код обязательно")
                .Matches(@"[0-9]{0,5}").WithMessage("Клиентская: Проверьте длину =5 либо формат данных!")
                .Length(5, 5).WithMessage("Клиентская: Проверьте длину =5!");
            RuleFor(x => x.Email).NotNull().WithMessage("Клиентская: Поле обязательно!")
                .EmailAddress().WithMessage("Клиентская: Только разрешенные символы!");
            RuleFor(x => x.Amount).NotNull().WithMessage("Клиентская: Сумма обязательно")
                .InclusiveBetween(.1m, 99999.99m).WithMessage("Клиентская: укажите от 0.01 до 99999.99");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Клиентская: Текст до 250 символов");
            RuleFor(x => x.SecurityCode).Matches(@"[0-9]{0,3}").WithMessage("Клиентская: Проверьте длину =3 либо формат данных!")
                .Length(3, 3).WithMessage("Клиентская: Проверьте длину =3!");
            RuleFor(x => x.ExpirationMonth)
                .Matches(@"[0-9]{0,2}").WithMessage("Клиентская: Проверьте длину =2 либо формат данных!")
                .Length(2, 2).WithMessage("Клиентская: Проверьте длину =2!")
                .Must(MonthIsValid).WithMessage("Серверная: месяц уже истек!");
            RuleFor(x => x.ExpirationYear)
                .Matches(@"[0-9]{0,4}").WithMessage("Клиентская: Проверьте длину =4 либо формат данных!")
                .Length(4, 4).WithMessage("Клиентская: Проверьте длину =4!")
                .Must(YearIsValid).WithMessage("Серверная: год уже истек!");
            RuleFor(x => x.CreditCardNumber)
                .Matches(@"[0-9]{0,16}").WithMessage("Клиентская: Проверьте длину =16 либо формат данных!")
                .Length(16, 16).WithMessage("Клиентская: Проверьте длину =16!")
                .Must(CreditCardIsValid).WithMessage("Серверная: данные по карте не верны!");
        }

        private bool CreditCardIsValid(string card)
        {
            int sum = 0;
            int N = card.Length;//16
            for (int i = 0; i <= N - 1; i++)//c 0 по 15
            {
                bool result = Int32.TryParse(card[i].ToString(), out int p);
                if (result)
                {
                    int reschet = i % 2; // кратное 2  четное - не четное
                    if (reschet == 0) // если четное 16 
                    {
                        p = 2 * p;
                        if (p > 9)
                        {
                            p = p - 9;
                        }
                    }
                    sum = sum + p; // итогова сумма по числу
                }
                else
                {
                    return false;
                }
            }
            int ressum = sum % 10; // кратное 10

            if (ressum == 0) return true;
            

            //bool resultN = Int32.TryParse(card[N - 1].ToString(), out int p2);
            //if (resultN)
            //{
            //    if (p2 == ressum) return true;//совападение контрольных сумм
            //    else return false;
            //}

            //return ValidationResult.Success;


            return false;
        }

        private bool YearIsValid(string year)
        {
            if (null == year) return false;
            int res = year.CompareTo(DateTime.Now.Year.ToString());
            if (res == -1) return false;
            return true;
        }

        private bool MonthIsValid(string month)
        {
            if (null == month) return false;
            int res = month.CompareTo(DateTime.Now.Month.ToString());
            if (res == -1) return true;
            return false;
        }
    }
}