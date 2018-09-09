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
using Walkies.Common;

namespace Walkies.Web.Controllers
{
    public class ShelterController : Controller
    {
        private IConfiguration _config;

        private ShelterRepository _shelterRepo;
        private DogRepository _dogRepo;

        public ShelterController(IConfiguration config, ShelterRepository shelterRepo, DogRepository dogRepo)
        {
            _config = config;
            _shelterRepo = shelterRepo;
            _dogRepo = dogRepo;
        }

        public IActionResult Index()
        {
            if (PasswordHash.userauth == null)
                return RedirectToAction("Login, Account");
            return View();
        }

        public async Task<IActionResult> Doggos()
        {
            IEnumerable<Dog> dogs = await _dogRepo.GetAll();
            return View(dogs);
        }
    }
}