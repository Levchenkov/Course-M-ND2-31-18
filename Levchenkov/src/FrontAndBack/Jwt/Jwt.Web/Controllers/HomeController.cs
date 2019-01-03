using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jwt.Web.Models;
using Microsoft.AspNetCore.Cors;

namespace Jwt.Web.Controllers
{
    //[EnableCors("MyCors")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:58142");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
