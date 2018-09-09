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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
                return View();
            else
                return View(await _shelterRepo.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Shelter shelter, String submitAction)
        {
            if (submitAction.Equals("SaveAndReturn"))
            {
                if (await _shelterRepo.GetById(shelter.ShelterId) == null)
                    await _shelterRepo.Insert(shelter);
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Index");
            }
            else if (submitAction.Equals("SaveAndAdd"))
            {
                if (await _shelterRepo.GetById(shelter.ShelterId) == null)
                    await _shelterRepo.Insert(shelter);
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Edit");
            }
            else if (submitAction.Equals("Delete"))
                return RedirectToAction("Delete", new { id = shelter.ShelterId });
            else
                return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _shelterRepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}