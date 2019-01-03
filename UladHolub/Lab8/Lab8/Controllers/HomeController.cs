using FluentValidation.Results;
using Lab8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab8.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var payment = new PaymentViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                MiddleName = "Johnovich",
                LastName = "Smith",
                Address = "Unknown streat 65-97",
                City = "Kampala",
                Country = "Uganda",
                PostCode = "200200",
                Email = "user@example.com",
                Amount = 12345.54F,
                Description = "Azazaza",
                CreditCardNumber = "4561261212345467",
                ExpirationMonth = 12,
                ExpirationYear = 20,
                SecurityCode = "476"
            };

            return View(payment);
        }

        [HttpPost]
        public ActionResult Index(PaymentViewModel payment)
        {
            var validator = new PaymentValidator();
            var result = validator.Validate(payment);
            if (result.IsValid) { }
            else
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
            }
            return View(payment);
        }
    }
}