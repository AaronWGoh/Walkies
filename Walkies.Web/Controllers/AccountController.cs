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
using Walkies.Common;

namespace Walkies.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IConfiguration _config;
        private IMediator _mediatr;

        private AccountUserRepository _shelterRepo;

        private PasswordHash passhash = new PasswordHash();


        public AccountController(IConfiguration config, AccountUserRepository shelterRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _shelterRepo = shelterRepo;
        }


        [Route("/Account/Login")]
        [HttpGet]
        public async Task<IActionResult> Login(Guid shelterId)
        {
            ViewBag.UserAccounts = await _shelterRepo.GetAll();
            if (shelterId.Equals(Guid.Empty))
                return View(new AccountUser());
            else
                return View(await _shelterRepo.GetById(shelterId));
        }


        [Route("/Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(AccountUser accountUser, string submitAction)
        {
            //if (submitAction.Equals("Delete Account"))
            //return RedirectToAction("Delete", new { AccountUserId = accountUser.AccountUserId });
            if (submitAction.Equals("Login"))
            {
                AccountUser acco = await _shelterRepo.GetUnlockedAccountsByEmail(accountUser);
                if (acco != null)
                {
                    if (passhash.DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
                    {
                        //Success
                        PasswordHash.userauth = acco;
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }

            }
            return RedirectToAction("Login", "Account");
        }


        [Route("/Account/Register")]
        [HttpGet]
        public async Task<IActionResult> Register(Guid shelterId)
        {
            return View(new AccountUser());
        }


        [Route("/Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(AccountUser shelter, String submitAction)
        {
            if (submitAction.Equals("Register"))
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
                        PasswordHash = passhash.GetNewPassHash(shelter.PasswordHash),
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

    }
}
