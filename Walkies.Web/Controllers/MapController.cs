﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Walkies.Web.Controllers
{
    public class MapController : Controller
    {
        public IActionResult MapPage()
        {
            return View();
        }

        public IActionResult DogWalker()
        {
            return View();
        }
    }
}