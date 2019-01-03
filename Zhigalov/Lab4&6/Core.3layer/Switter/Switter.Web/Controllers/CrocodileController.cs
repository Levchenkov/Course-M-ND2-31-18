using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Switter.Web.Controllers
{
    public class CrocodileController : Controller
    {
        public IActionResult Index()
        {
            return View("Crocodile");
        }
    }
}