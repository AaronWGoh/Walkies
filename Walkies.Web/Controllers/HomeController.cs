using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Walkies.Common;
using Walkies.Common.Models;
using Walkies.DatabaseOperations;
using Walkies.Web.Models;

namespace Walkies.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _config;
        private UserTypeRepository _userTypeRepo;
        private AccountUserRepository _accountUserRepo;

        private PasswordHash passwordHash = new PasswordHash();

        public HomeController(IConfiguration config, UserTypeRepository userTypeRepo, AccountUserRepository accountUserRepo)
        {
            _config = config;
            _userTypeRepo = userTypeRepo;
            _accountUserRepo = accountUserRepo;
        }

        public async Task<IActionResult> Index() { 

            AccountUser use = new AccountUser();
            ViewBag.userTypes = await _userTypeRepo.GetAll();
            return View(new AccountUser());
        }

        [HttpPost]
        public async Task<IActionResult> Index(AccountUser accountUser)
        {
            AccountUser acco = await _accountUserRepo.GetUnlockedAccountsByEmail(accountUser);
            if (acco != null)
            {
                if (passwordHash.DoesPasswordMatch(acco.PasswordHash, acco.LoginEmail))
                {
                    return RedirectToRoute("/Shelter/Index");
                }
            }
            return RedirectToRoute("/Account/Login");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
