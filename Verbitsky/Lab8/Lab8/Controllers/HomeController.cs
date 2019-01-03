using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab8.Models;

namespace Lab8.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult PaymentAcceptance()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PaymentAcceptance(PaymentAcceptanceViewModel paymentAcceptance)
        {
            var validator = new PaymentAcceptanceValidator();
            var validatorResult = validator.Validate(paymentAcceptance);
            if (validatorResult.IsValid)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return View("PaymentAcceptance", paymentAcceptance);
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
