using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common.Models;
using Walkies.Common;
using Walkies.DatabaseOperations;
using Walkies.DatabaseOperations.Handlers;
using BCrypt.Net;
using BCrypt;

namespace Walkies.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IConfiguration _config;
        private IMediator _mediatr;
        private PasswordHash passwordHash = new PasswordHash();

        private AccountUserRepository _shelterRepo;

        public AccountController(IConfiguration config, AccountUserRepository shelterRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _shelterRepo = shelterRepo;
        }


        [Route("/Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [Route("/Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(AccountUser accountUser)
        {
            AccountUser acco = await _shelterRepo.GetUnlockedAccountsByEmail(accountUser);
            if (acco != null)
            {
                if (passwordHash.DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
                {
                    //Success!
                    return RedirectToRoute("/Shelter/Index");
                }
            }
            return RedirectToRoute("/Account/Login");
        }


        [Route("/Account/Register")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.UserAccounts = await _shelterRepo.GetAll();
            return View(new AccountUser());
        }


        [Route("/Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(AccountUser shelter, String submitAction)
        {
            if (submitAction.Equals("Submit"))
            {
                AccountUserHandler.AddCmd addCmd = new AccountUserHandler.AddCmd
                {
                    FirstName = shelter.FirstName,
                    AccountUserId = shelter.AccountUserId,
                    LastName = shelter.LastName,
                    LoginEmail = shelter.LoginEmail,
                    RecoveryPhone = shelter.RecoveryPhone,
                    PasswordHash = passwordHash.GetNewPassHash(shelter.PasswordHash),
                    CanLogin = shelter.CanLogin,
                    IsLockedDateTime = shelter.IsLockedDateTime,
                    ResetToken = shelter.ResetToken,
                    ResetTokenExpiration = shelter.ResetTokenExpiration
                };
                await _mediatr.Send(addCmd);
            } 

            //In case of cancelation, just returns
            return RedirectToAction("Index");
        }

    }
}
