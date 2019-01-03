using Lab8.DataLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Lab8.Models
{
    public class PayAcc
    {
        //FirstName
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [Display(Name = "Имя", Description = "Укажите Имя")]
        public string FirstName { get; set; }
        //MiddleName
        [Display(Name = "Отчество", Description = "Укажите Отчество")]
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        public string MiddleName { get; set; }
        //LastName
        [Display(Name = "Фамилия", Description = "Укажите Фамилия")]
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        public string LastName { get; set; }
        //Address
        [Display(Name = "Адрес", Description = "Укажите адрес")]
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [RegularExpression(@"[.,\-0-9A-Za-zА-Яа-я\s]+", ErrorMessage = "{0}: Укажите допустимые знаки!")]
        public string Address { get; set; }
        //City
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [RegularExpression(@"[\-0-9A-Za-zА-Яа-я\s]+", ErrorMessage = "{0}: Укажите допустимые знаки!")]
        public string City { get; set; }
        //Country
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [RegularExpression(@"[\-0-9A-Za-zА-Яа-я\s]+", ErrorMessage = "{0}: Укажите допустимые знаки!")]
        public string Country { get; set; }
        //PostCode
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [RegularExpression(@"[0-9]{0,5}", ErrorMessage = "{0}: Укажите цифры!")]
        [Display(Name = "Почтовый код", Description = "Укажите почтовый код")]
        [StringLength(5, ErrorMessage = "{0}: длина поля 5-ть символов!", MinimumLength = 5)]
        public string PostCode { get; set; }
        //Email
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [Display(Name = "Электронный ящик", Description = "Укажите электронный ящик")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ошибка в написании электронного ящика")]
        public string Email { get; set; }
        //Amount
        private decimal amount;
        [Required(ErrorMessage = "{0}: Поле обязательно для заполнения")]
        [Display(Name = "Сумма", Description = "Укажите сумму")]
        [CustomValidation(typeof(DL), "ValidateAmount")]
        public decimal Amount
        {
            get { return amount; }
            set
            {
                //NumberFormatInfo nfi = new CultureInfo("ru-RU", false).NumberFormat;
                amount = value;
            }
        }
        //Description
        [StringLength(250, ErrorMessage = "{0}: длина поля до 250-и символов!")]
        [Display(Name = "Описание", Description = "Укажите описание")]
        public string Description { get; set; }
        //CreditCardNumber
        [RegularExpression(@"[0-9]{0,16}", ErrorMessage = "{0}: Укажите цифры!")]
        [StringLength(16, ErrorMessage = "{0}: длина поля 16-ть символов!", MinimumLength = 16)]
        [Display(Name = "Номер карты", Description = "Укажите номер карты")]
        [CustomValidation(typeof(DL), "ValidateCreditCardNumber")]
        public string CreditCardNumber { get; set; }
        //ExpirationMonth
        [RegularExpression(@"[0-9]+", ErrorMessage = "{0}: Укажите цифры!")]
        [Display(Name = "Месяц", Description = "Укажите месяц")]
        [CustomValidation(typeof(DL), "ValidateMonth")]
        public string ExpirationMonth { get; set; }
        //ExpirationYear
        [RegularExpression(@"[0-9]+", ErrorMessage = "{0}: Укажите цифры!")]
        [StringLength(4, ErrorMessage = "{0}: длина поля 4-е символа!", MinimumLength = 4)]
        [Display(Name = "Год", Description = "Укажите год")]
        [CustomValidation(typeof(DL), "ValidateYear")]
        public string ExpirationYear { get; set; }
        //SecurityCode
        [RegularExpression(@"[0-9]+", ErrorMessage = "{0}: Укажите цифры!")]
        [Display(Name = "Код", Description = "Укажите код")]
        [StringLength(3, ErrorMessage = "{0}: длина поля 3-и символа!", MinimumLength = 3)]
        public string SecurityCode { get; set; }
    }
}