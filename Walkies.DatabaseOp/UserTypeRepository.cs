using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Walkies.Common;
using Walkies.Common.Models;

namespace Walkies.DatabaseOperations
{
    public class UserTypeRepository : RdbmsRepository, IRepository
    {
        public UserTypeRepository(IConfiguration cfg, Queries qs) : base(cfg, qs) { }

        public async Task<IEnumerable<UserType>> GetAll()
        {
            return await QueryAsync<UserType>(_qs.UserType.GetAll);
        }

    }
}
