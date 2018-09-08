using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Admin.Models;

namespace Walkies.Admin.Controllers
{
    public class ShelterController : Controller
    {
        private IConfiguration _config;

        public ShelterController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}