using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Lab8.DataLogic
{
    public class DL
    {
        public static ValidationResult ValidateYear(string year)
        {
            string NowYear = DateTime.Now.Year.ToString();
            int res = String.Compare(year, NowYear);
            if (res == 0) return ValidationResult.Success;
            else if (res == -1) return new ValidationResult("Ошибка - год уже прошел!");
            else if (res == 1) return ValidationResult.Success;

            return ValidationResult.Success;
        }
        public static ValidationResult ValidateMonth(string month)
        {
            string NowMonth = DateTime.Now.Month.ToString();

            int res = String.Compare(month, NowMonth);
            if (res == 0) return ValidationResult.Success;
            else if (res == -1) return new ValidationResult("Ошибка - месяц уже прошел!");
            else if (res == 1) return ValidationResult.Success;


            return ValidationResult.Success;
        }
        public static ValidationResult ValidateAmount(string Amount)
        {
            //decimal myAmount = 0;
            bool myRes = Decimal.TryParse(Amount, out decimal myAmount);
            if (myRes)
            {


                int res1 = Decimal.Compare(myAmount, .1m);
                if (res1 == -1)
                {
                    return new ValidationResult("Ошибка - сумма должна быть между 0.01 и 99999.99!");
                }
                else
                {
                    int res2 = Decimal.Compare(myAmount, 99999.99m);
                    if (res2 == 1) return new ValidationResult("Ошибка - сумма должна быть между 0.01 и 99999.99!");
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                return new ValidationResult("Ошибка - сумма должна быть между 0.01 и 99999.99!");
            }
        }
        public static ValidationResult ValidateCreditCardNumber(string card)
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
                    return new ValidationResult("Ошибка - в номере карты!");
                }
            }
            int ressum = sum % 10; // кратное 10

            bool resultN = Int32.TryParse(card[N - 1].ToString(), out int p2);
            if (resultN)
            {
                if (p2 == ressum) return ValidationResult.Success;//совападение контрольных сумм
                else return new ValidationResult("Ошибка - в номере карты!");
            }

            return ValidationResult.Success;
        }
    }
}