using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Walkies.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginRegister()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}