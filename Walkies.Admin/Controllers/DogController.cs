using System;
using System.Collections.Generic;
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
    public class DogController : Controller
    {
        private IMediator _mdr;
        private IConfiguration _config;
        private DogRepository _dogRepo;
        private ShelterRepository _shelterRepo;

        public DogController(IMediator mdr, IConfiguration config, DogRepository dogRepo, ShelterRepository shelterRepo)
        {
            _mdr = mdr;
            _config = config;
            _dogRepo = dogRepo;
            _shelterRepo = shelterRepo;
        }
        [Route("/Shelter/{shelterId}/Dog")]
        public async Task<IActionResult> Index(Guid shelterId)
        {
            IEnumerable<Dog> dogs = await _dogRepo.GetAllByShelterId(shelterId);
            ViewBag.shelterId = shelterId;
            return View(dogs);
        }

        [Route("/Shelter/{shelterId}/Dog/Add")]
        [Route("/Shelter/{shelterId}/Dog/{dogId}/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid dogId, Guid shelterId)
        {
            IEnumerable<Shelter> shelters = await _shelterRepo.GetAll();
            ViewBag.shelters = shelters;

            ViewBag.shelterId = shelterId;
            if (dogId.Equals(Guid.Empty))
                return View(new Dog());
            else
                return View(await _dogRepo.GetById(dogId));
        }
        [Route("/Shelter/{shelterId}/Dog/Add")]
        [Route("/Shelter/{shelterId}/Dog/{dogId}/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(Dog dog, Guid shelterId, String submitAction)
        {
            if (submitAction.Equals("Cancel"))
                return RedirectToAction("Index", new { shelterId });

            //Inserts or Updates
            if (await _dogRepo.GetById(dog.DogId) == null)
            {
                DogHandler.AddCmd addCmd = new DogHandler.AddCmd
                {
                    Name = dog.Name,
                    AvailableDate = dog.AvailableDate,
                    Breed = dog.Breed,
                    Description = dog.Description,
                    DogId = dog.DogId,
                    IsPublic = dog.IsPublic,
                    PrimaryImageFileId = dog.PrimaryImageFileId,
                    ShelterId = dog.ShelterId
                };
                await _mdr.Send(addCmd);
            }
            else
            {
                DogHandler.UpdateCmd updateCmd = new DogHandler.UpdateCmd
                {
                    Name = dog.Name,
                    AvailableDate = dog.AvailableDate,
                    Breed = dog.Breed,
                    Description = dog.Description,
                    DogId = dog.DogId,
                    IsPublic = dog.IsPublic,
                    PrimaryImageFileId = dog.PrimaryImageFileId,
                    ShelterId = dog.ShelterId
                };
                await _mdr.Send(updateCmd);
            }

            //Save and Add
            if (submitAction.Equals("SaveAndAdd"))
                return RedirectToAction("Edit", new { shelterId });
            //Cancel and Return
            else
                return RedirectToAction("Index", new { shelterId });
        }


        [Route("/Shelter/{shelterId}/Dog/{dogId}/delete/")]
        public async Task<IActionResult> Delete(Guid dogId, Guid shelterId)
        {
            await _dogRepo.Delete(dogId);
            return RedirectToAction("Index", new { shelterId });
        }
    }
}