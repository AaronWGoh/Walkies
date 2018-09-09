using System;
using System.Collections.Generic;
using System.Text;
using Walkies.Common;

namespace Walkies.DatabaseOperations
{
    public class AccountUserQueries
    {
        public string GetAll
        {
            get
            {
                return "select * from AccountUser";
            }
        }

        public string GetById
        {
            get
            {
                return "select * from AccountUser where AccountUserId = @AccountUserId";
            }
        }

        public string GetByUnlockedandEmail
        {
            get
            {
                return "select * from AccountUser where CanLogin = '1' and LoginEmail = @LoginEmail";
            }
        }

        public string Insert
        {
            get
            {
                return @"insert into AccountUser (AccountUserId, UserTypeCode, FirstName, LastName, LoginEmail, RecoveryPhone, PasswordHash, CanLogin, IsLockedDateTime, ResetToken, ResetTokenExpiration)" +
                    "       values (@AccountUserId, @UserTypeCode, @FirstName, @LastName, @LoginEmail, @RecoveryPhone, @PasswordHash, @CanLogin, @IsLockedDateTime, @ResetToken, @ResetTokenExpiration)";
            }
        }

        public string Update
        {
            get
            {
                return @"update AccountUser
                    set 
                    AccountUserId = @AccountUserId,
                    UserTypeCode = @UserTypeCode,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    LoginEmail = @LoginEmail,
                    RecoveryPhone = @RecoveryPhone,
                    PasswordHash = @PasswordHash,
                    CanLogin = @CanLogin,
                    IsLockedDateTime = @IsLockedDateTime,
                    ResetToken = @ResetToken,
                    ResetTokenExpiration = @ResetTokenExpiration
                    where AccountUserId = @AccountUserId";
            }
        }

        public string Delete
        {
            get
            {
                return @"delete from AccountUser
                    where AccountUserId = @AccountUserId";
            }
        }
    }

    public partial class Queries : IQueries
    {
        public AccountUserQueries AccountUser = new AccountUserQueries();
    }
}
