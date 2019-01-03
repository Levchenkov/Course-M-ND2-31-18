using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetUsers()
        {
            return View(UserRepository.Users);
        }
    }
}
