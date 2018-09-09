using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common.Models;
using Walkies.DatabaseOperations;
using Walkies.Common;

namespace Walkies.Web.Controllers
{
    public class MapController : Controller
    {
        private IConfiguration _config;
        private ShelterRepository _shelterRepo;
        private DogRepository _dogRepo;
        private UserTypeRepository _userTypeRepo;

        public MapController(IConfiguration config, ShelterRepository shelterRepo, DogRepository dogRepo, UserTypeRepository userTypeRepo)
        {
            _config = config;
            _shelterRepo = shelterRepo;
            _dogRepo = dogRepo;
            _userTypeRepo = userTypeRepo;
        }
        public async Task<IActionResult> Index()
        {
            if (PasswordHash.userauth == null)
                return RedirectToAction("Login", "Account");

            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            IEnumerable<Dog> dogs = await _dogRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();

            DisplayData displayData = new DisplayData
            {
                Shelters = shelters,
                Dogs = dogs,
                City = "Salt Lake City",
                StateCode = "UT"
            };

            return View(displayData); 
        }

        public async Task<IActionResult> DogWalker()
        {
            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            IEnumerable<Dog> dogs = await _dogRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();

            DisplayData displayData = new DisplayData
            {
                Shelters = shelters,
                Dogs = dogs,
                City = "Salt Lake City",
                StateCode = "UT"
            };

            return View(displayData);
        }
    }
}