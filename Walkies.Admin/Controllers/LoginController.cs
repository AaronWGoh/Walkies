using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Walkies.Common.Models;
using Walkies.DatabaseOperations;
using Walkies.DatabaseOperations.Handlers;

namespace Walkies.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IMediator _mediatr;

        private AccountUserRepository _shelterRepo;

        public LoginController(IConfiguration config, AccountUserRepository shelterRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _shelterRepo = shelterRepo;
        }

        [Route("/AccountUser/Add")]
        [Route("/AccountUser/Edit")]
        [HttpGet]
        public async Task<IActionResult> Add(Guid shelterId)
        {
            ViewBag.AccountUser = await _shelterRepo.GetAll();
            if (shelterId.Equals(Guid.Empty))
                return View(new AccountUser());
            else
                return View(await _shelterRepo.GetById(shelterId));
        }

        [Route("Login/Attempt")]
        [HttpGet]
        public async Task<IActionResult> Login(AccountUser accountUser, string submitAction)
        {
              if (submitAction.Equals("Delete Account"))
                return RedirectToAction("Delete", new { AccountUserId = accountUser.AccountUserId });
              if(submitAction.Equals("Login"))
            {
                AccountUser acco = await _shelterRepo.GetByUnlockedandEmail(accountUser);
                if(acco.AccountUserId != null)
                {
                    if (DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
                    {
                        //Success!
                        return RedirectToRoute("Shelter");
                    }
                    else
                    {
                        return RedirectToRoute("Login");
                    }
                }

            }
            return RedirectToAction("Login");
        }


        [Route("/Account/Add")]
        [Route("/Account/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(AccountUser shelter, String submitAction)
        {
            if (submitAction.Equals("Sign Up"))
            {
                if (shelter.AccountUserId.Equals(Guid.Empty))
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
                else
                    await _shelterRepo.Update(shelter);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");

        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountUser> accountUsers = await _shelterRepo.GetAll();
            return View(accountUsers);
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