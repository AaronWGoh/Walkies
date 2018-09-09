using System;
using System.Collections.Generic;
using System.Text;
using Walkies.Common.Models;

namespace Walkies.Common
{
    public class PasswordHash
    {
        public static AccountUser userauth;
        public string GetNewPassHash(string pass)
        {
            string pwdToHash = pass + "^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            return BCrypt.Net.BCrypt.HashPassword(pwdToHash);
        }

        public void SetPassword(string user, string userPassword)
        {
            string pwdToHash = userPassword + "^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            string hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash);
        }

        public bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
        }
    }
}
