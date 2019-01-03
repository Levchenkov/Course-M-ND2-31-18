using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab6Crocodile.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            SelectList Groups = new SelectList(Models.GroupManager.Groups);
            return View(Groups);
        }
    }
}
