using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Models
{
    public class CreditCardValidator : PropertyValidator
    {
        public CreditCardValidator()
            :base("Wrong credit card number")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            ulong digit;
            if (!ulong.TryParse(context.PropertyValue.ToString(), out digit))
            {
                return false;
            }
            if (digit <= 999999999999999 && digit >= 10000000000000000)
            {
                return false;
            }
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

            return arrayInt.Sum() % 10 == 0;
        }
    }
}
