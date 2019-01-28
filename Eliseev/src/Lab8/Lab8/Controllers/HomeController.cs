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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pay(CustomPaymentInfo customPaymentInfo)
        {
            CustomPaymentInfoValidator validator = new CustomPaymentInfoValidator();
            ValidationResult result = validator.Validate(customPaymentInfo);
            if (!ModelState.IsValid)
            {
                foreach(ValidationFailure failure in result.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View("Index",customPaymentInfo);

            }

            return View();
        }
    }
}