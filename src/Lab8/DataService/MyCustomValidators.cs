using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.DataService
{
    public static class MyCustomValidators
    {
        //public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        //{
        //    return ruleBuilder.Must(list => list.Count < num).WithMessage("The list contains too many items");
        //}
        //public static IRuleBuilderInitial<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        //{

        //    return ruleBuilder.Custom((list, context) => {
        //        if (list.Count > 10)
        //        {
        //            context.AddFailure("The list must contain 10 items or fewer");
        //        }
        //    });
        //}
        //public static IRuleBuilderInitial<T, string> MonthMoreThanNow<T, TElement>(this IRuleBuilder<T, string> ruleBuilder)
        //{

        //    return ruleBuilder.Custom((month, context) => {
        //        //var res = DateTime.Now.Month.ToString();
        //        var res = String.Compare(month, DateTime.Now.Month.ToString());
        //        if (res == -1)
        //        {
        //            context.AddFailure($"Клиентская: месяц уже истек");
        //        }
        //    });
        //}
        //public static IRuleBuilderOptions<T, string> MonthMoreThanNow<T, TElement>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    //var now = DateTime.Now.Month.ToString();
        //    //int res = String.Compare(month, NowMonth);
        //    //if (res == 0) return true;
        //    //else if (res == -1) return new ValidationResult("Ошибка - месяц уже прошел!");
        //    //else if (res == 1) return ValidationResult.Success;
        //    return ruleBuilder.Must(month => month.CompareTo(DateTime.Now.Month.ToString()) == -1).WithMessage("Клиентская: Месяц должен быть больше текущего!");
        //    //return ruleBuilder.Custom((month, context) =>
        //    //{
        //    //    var now = DateTime.Now.Month.ToString();

        //    //    if (month.CompareTo(now) == -1)
        //    //    {
        //    //        context.AddFailure(new ValidationFailure($"PayAcc.ExpirationMonth", $"Клиентская: месяц уже истек"));
        //    //    }

        //    //});
        //    //).WithMessage("Клиентская: Месяц должен быть больше текущего!");


        //}
    }
}