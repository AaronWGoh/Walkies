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

        public MapController(IConfiguration config, ShelterRepository shelterRepo, DogRepository dogRepo)
        {
            _config = config;
            _shelterRepo = shelterRepo;
            _dogRepo = dogRepo;
        }
        public async Task<IActionResult> Index()
        {
            if (PasswordHash.userauth == null)
                return RedirectToRoute("/Account/Login");
            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            IEnumerable<Dog> dogs = await _dogRepo.GetAll();

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