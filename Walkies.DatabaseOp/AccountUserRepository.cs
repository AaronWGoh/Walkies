using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkies.Common.Models;
using Walkies.Common;

namespace Walkies.DatabaseOperations
{
    public class AccountUserRepository : RdbmsRepository, IRepository
    {
        public AccountUserRepository(IConfiguration cfg, Queries qs) : base(cfg, qs) { }

        public async Task<IEnumerable<AccountUser>> GetAll()
        {
            return await QueryAsync<AccountUser>(_qs.AccountUser.GetAll);
        }

        public async Task<AccountUser> GetById(Guid id)
        {
            var result = await QueryAsync<AccountUser>(_qs.AccountUser.GetById, new { AccountUserId = id });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns account users that are not locked out of the account and grabs by email
        /// </summary>
        /// <param name="accountUser"></param>
        /// <returns></returns>
        public async Task<AccountUser> GetUnlockedAccountsByEmail(AccountUser accountUser)
        {
            var result = await QueryAsync<AccountUser>(_qs.AccountUser.GetUnlockedAccountsByEmail, accountUser);
            return result.FirstOrDefault();
        }

        public async Task<AccountUser> Insert(AccountUser shelter)
        {
            var result = await QueryAsync<AccountUser>(_qs.AccountUser.Insert, shelter);
            return result.FirstOrDefault();
        }

        public async Task<AccountUser> Update(AccountUser shelter)
        {
            var result = await QueryAsync<AccountUser>(_qs.AccountUser.Update, shelter);
            return result.FirstOrDefault();
        }

        public async Task Delete(Guid id)
        {
            await ExecuteAsync(_qs.AccountUser.Delete, new { @AccountUserId = id });
        }
    }
}
