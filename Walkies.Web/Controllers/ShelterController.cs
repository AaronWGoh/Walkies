using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common.Models;
using Walkies.DatabaseOperations;

namespace Walkies.Web.Controllers
{
    public class ShelterController : Controller
    {
        private IConfiguration _config;

        private ShelterRepository _shelterRepo;

        public ShelterController(IConfiguration config, ShelterRepository shelterRepo)
        {
            _config = config;
            _shelterRepo = shelterRepo;
        }

        public IActionResult Shelter()
        {
            return View();
        }

        public IActionResult Doggos()
        {
            return View();
        }
    }
}