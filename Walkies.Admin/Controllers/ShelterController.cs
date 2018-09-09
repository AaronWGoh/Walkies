using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common.Models;
using Walkies.DatabaseOperations;
using Walkies.DatabaseOperations.Handlers;

namespace Walkies.Admin.Controllers
{
    public class ShelterController : Controller
    {
        private IConfiguration _config;
        private IMediator _mediatr;

        private ShelterRepository _shelterRepo;

        public ShelterController(IConfiguration config, ShelterRepository shelterRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _shelterRepo = shelterRepo;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            return View(shelters);
        }

        [Route("/Shelter/Add")]
        [Route("/Shelter/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid shelterId)
        {
            ViewBag.Shelters = await _shelterRepo.GetAll();
            if (shelterId.Equals(Guid.Empty))
                return View(new Shelter());
            else
                return View(await _shelterRepo.GetById(shelterId));
        }

        [Route("/Shelter/Add")]
        [Route("/Shelter/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(Shelter shelter, String submitAction)
        {
            if (submitAction.Equals("SaveAndReturn"))
            {
                if (shelter.ShelterId.Equals(Guid.Empty))
                {
                    ShelterHandler.AddCmd addCmd = new ShelterHandler.AddCmd
                    {
                        Name = shelter.Name,
                        City = shelter.City,
                        Email = shelter.Email,
                        Fax = shelter.Fax,
                        Latitude = shelter.Latitude,
                        Longitude = shelter.Longitude,
                        Phone = shelter.Phone,
                        ShelterId = shelter.ShelterId,
                        StateCode = shelter.StateCode,
                        Street1 = shelter.Street1,
                        Street2 = shelter.Street2,
                        Zip = shelter.Zip
                    };
                    await _mediatr.Send(addCmd);
                }
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Index");
            }
            else if (submitAction.Equals("SaveAndAdd"))
            {
                if (shelter.ShelterId.Equals(Guid.Empty))
                {
                    ShelterHandler.AddCmd addCmd = new ShelterHandler.AddCmd
                    {
                        Name = shelter.Name,
                        City = shelter.City,
                        Email = shelter.Email,
                        Fax = shelter.Fax,
                        Latitude = shelter.Latitude,
                        Longitude = shelter.Longitude,
                        Phone = shelter.Phone,
                        ShelterId = shelter.ShelterId,
                        StateCode = shelter.StateCode,
                        Street1 = shelter.Street1,
                        Street2 = shelter.Street2,
                        Zip = shelter.Zip
                    };
                    await _mediatr.Send(addCmd);
                }
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Edit");
            }
            else if (submitAction.Equals("Delete"))
                return RedirectToAction("Delete", new { shelterId = shelter.ShelterId });
            else
                return RedirectToAction("Index");

        }

        [Route("/Shelter/Delete")]
        public async Task<IActionResult> Delete(Guid shelterId)
        {
            await _shelterRepo.Delete(shelterId);
            return RedirectToAction("Index");
        }

    }
}