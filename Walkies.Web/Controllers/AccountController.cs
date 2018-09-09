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

        private AccountUserRepository _accountUserRepo;
        private UserTypeRepository _userTypeRepo;

        private PasswordHash passhash = new PasswordHash();


        public AccountController(IConfiguration config, AccountUserRepository accountUserRepo, UserTypeRepository userTypeRepo, IMediator mediatr)
        {
            _config = config;
            _mediatr = mediatr;

            _accountUserRepo = accountUserRepo;
            _userTypeRepo = userTypeRepo;
        }


        [Route("/Account/Login")]
        [HttpGet]
        public async Task<IActionResult> Login(Guid shelterId)
        {
            ViewBag.UserAccounts = await _accountUserRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();
            if (shelterId.Equals(Guid.Empty))
                return View(new AccountUser());
            else
                return View(await _accountUserRepo.GetById(shelterId));
        }


        [Route("/Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(AccountUser accountUser)
        {
            //if (submitAction.Equals("Delete Account"))
            //return RedirectToAction("Delete", new { AccountUserId = accountUser.AccountUserId });
                AccountUser acco = await _accountUserRepo.GetByEmail(accountUser);
                if (acco != null)
                {
                    if (passhash.DoesPasswordMatch(acco.PasswordHash, accountUser.PasswordHash))
                    {
                        //Success
                        PasswordHash.userauth = acco;
                        return RedirectToAction("Index", "Shelter");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            return RedirectToAction("Login", "Account");
        }


        [Route("/Account/Register")]
        [HttpGet]
        public async Task<IActionResult> Register(Guid shelterId)
        {
            ViewBag.UserAccounts = await _accountUserRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();

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
                    await _accountUserRepo.Update(shelter);

                return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Login");

        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountUser> accountUsers = await _accountUserRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();
            ViewBag.userTypes = await _userTypeRepo.GetAll();

            return View(accountUsers);
        }

    }
}
