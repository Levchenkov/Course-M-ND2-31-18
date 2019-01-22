using Lab8.DataLogic;
using Lab8.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab8.Models
{
    [FluentValidation.Attributes.Validator(typeof(PayAccValidator))]
    public class PayAcc
    {
        //FirstName
        [Display(Name = "Имя", Description = "Укажите Имя")]
        public string FirstName { get; set; }
        //MiddleName
        [Display(Name = "Отчество", Description = "Укажите Отчество")]
        public string MiddleName { get; set; }
        //LastName
        [Display(Name = "Фамилия", Description = "Укажите Фамилия")]
        public string LastName { get; set; }
        //Address
        [Display(Name = "Адрес", Description = "Укажите адрес")]
        public string Address { get; set; }
        //City
        [Display(Name = "Город", Description = "Укажите город")]
        public string City { get; set; }
        //Country
        [Display(Name = "Страна", Description = "Укажите страну")]
        public string Country { get; set; }
        //PostCode
        [Display(Name = "Почтовый код", Description = "Укажите почтовый код")]
        public string PostCode { get; set; }
        //Email
        [Display(Name = "Электронный ящик", Description = "Укажите электронный ящик")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Ошибка в написании электронного ящика")]
        public string Email { get; set; }
        //Amount
        [Display(Name = "Сумма", Description = "Укажите сумму")]
        public decimal Amount { get; set; }

        //Description
        [Display(Name = "Описание", Description = "Укажите описание")]
        public string Description { get; set; }
        //CreditCardNumber
        [Display(Name = "Номер карты", Description = "Укажите номер карты")]
        //[CustomValidation(typeof(DL), "ValidateCreditCardNumber")]
        public string CreditCardNumber { get; set; }
        //ExpirationMonth
        //[RegularExpression(@"[0-9]+", ErrorMessage = "{0}: Укажите цифры!")]
        [Display(Name = "Месяц", Description = "Укажите месяц")]
        //[CustomValidation(typeof(DL), "ValidateMonth")]
        public string ExpirationMonth { get; set; }
        //ExpirationYear
        [Display(Name = "Год", Description = "Укажите год")]
        //[CustomValidation(typeof(DL), "ValidateYear")]
        public string ExpirationYear { get; set; }
        //SecurityCode
        [Display(Name = "Код", Description = "Укажите код")]
        public string SecurityCode { get; set; }
    }
}