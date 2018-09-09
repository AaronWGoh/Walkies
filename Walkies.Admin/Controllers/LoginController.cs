using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using Walkies.DatabaseOperations;
using BCrypt.Net;

namespace Walkies.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
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