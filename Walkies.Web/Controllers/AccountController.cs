using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common.Models;
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
                if (DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
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
                    PasswordHash = GetNewPassHash(shelter.PasswordHash),
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

        private string GetNewPassHash(string pass)
        {
            string pwdToHash = pass + "^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            return BCrypt.Net.BCrypt.HashPassword(pwdToHash);
        }

        private void SetPassword(string user, string userPassword)
        {
            string pwdToHash = userPassword + "^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            string hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash);
        }

        private bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
        }
    }
}
