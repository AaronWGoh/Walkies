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

        private AccountUserRepository _accountRepo;

        private AccountUser userAuth = null;

        public LoginController(IConfiguration config, AccountUserRepository shelterRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _accountRepo = shelterRepo;
        }


        [Route("/Login/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid accountId)
        {
            ViewBag.UserAccounts = await _accountRepo.GetAll();
            if (accountId.Equals(Guid.Empty))
                return View(new AccountUser());
            else
                return View(await _accountRepo.GetById(accountId));
        }


        [Route("/Login/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(AccountUser accountUser, string submitAction)
        {
              //if (submitAction.Equals("Delete Account"))
                //return RedirectToAction("Delete", new { AccountUserId = accountUser.AccountUserId });
              if(submitAction.Equals("Login"))
            {
                AccountUser acco = await _accountRepo.GetUnlockedAccountsByEmail(accountUser);
                if(acco != null)
                {
                    if (DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
                    {
                        //Success
                        userAuth = acco;
                        return RedirectToRoute("/Login/Index");
                    }
                    else
                    {
                        return RedirectToRoute("/Login/Index");
                    }
                }

            }
            return RedirectToRoute("/Login/Edit");
        }


        [Route("/Login/Create")]
        [HttpGet]
        public async Task<IActionResult> Create(Guid shelterId)
        {
                return View(new AccountUser());
        }


        [Route("/Login/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(AccountUser shelter, String submitAction)
        {
            if (submitAction.Equals("Create"))
            {
                if (shelter.AccountUserId.Equals(Guid.Empty))
                {
                    AccountUserHandler.AddCmd addCmd = new AccountUserHandler.AddCmd
                    {
                        FirstName = shelter.FirstName,
                        AccountUserId = shelter.AccountUserId,
                        UserTypeCode = shelter.UserTypeCode,
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
                    await _accountRepo.Update(shelter);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");

        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountUser> accountUsers = await _accountRepo.GetAll();
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

        public AccountUser GetProfile()
        {
            return userAuth;
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _accountRepo.Delete(id);
            return RedirectToAction("Index");
        }

        private bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
        }
    }
}