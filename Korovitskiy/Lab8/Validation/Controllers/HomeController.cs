using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Validation.Models;

namespace Validation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Pay()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pay(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View("Pay", payment);
            }
            //success
            return View();
        }
    }
}
