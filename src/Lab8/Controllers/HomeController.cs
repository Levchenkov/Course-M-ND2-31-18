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
            PayAcc payacc = new PayAcc()
            {
                FirstName = "Alex",
                MiddleName = "A",
                LastName = "M",
                Amount = 10000,
                City = "Минск",
                Email = "email@post.by",
                Address = "проспект",
                Country = "Belarus",
                CreditCardNumber = "4561261212345467",
                Description = "Описание",
                ExpirationMonth = "01",
                ExpirationYear = "2019",
                PostCode = "12345",
                SecurityCode = "189"
            };

            return View(payacc);
        }
        [HttpPost]
        public ActionResult Index(PayAcc model)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Платеж выполнен");
                return View(model);
            }
            ModelState.AddModelError("", "Ошибка в данных");
            return View(model);
        }
    }
}