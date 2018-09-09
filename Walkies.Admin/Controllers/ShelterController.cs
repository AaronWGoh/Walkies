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

namespace Walkies.Admin.Controllers
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            return View(shelters);
        }

        [Route("/Shelter/Add")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid shelterId)
        {
            if (shelterId.Equals(Guid.Empty))
                return View(new Shelter());
            else
                return View(await _shelterRepo.GetById(shelterId));
        }

        [Route("/Shelter/Add")]
        [HttpPost]
        public async Task<IActionResult> Edit(Shelter shelter, String submitAction)
        {
            Shelter newShelter = await _shelterRepo.GetById(shelter.ShelterId);
            if (submitAction.Equals("SaveAndReturn"))
            {
                if (newShelter == null)
                    await _shelterRepo.Insert(shelter);
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Index");
            }
            else if (submitAction.Equals("SaveAndAdd"))
            {
                if (newShelter == null)
                    await _shelterRepo.Insert(shelter);
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Edit");
            }
            else if (submitAction.Equals("Delete"))
                return RedirectToAction("Delete", new { shelterId = shelter.ShelterId });
            else
                return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(Guid shelterId)
        {
            await _shelterRepo.Delete(shelterId);
            return RedirectToAction("Index");
        }

    }
}