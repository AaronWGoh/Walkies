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
    public class ShelterRepository : RdbmsRepository, IRepository
    {
        public ShelterRepository(IConfiguration cfg, Queries qs) : base(cfg, qs) { }

        public async Task<IEnumerable<Shelter>> GetAll()
        {
            return await QueryAsync<Shelter>(_qs.Shelter.GetAll);
        }

        public async Task<Shelter> GetById(Guid id)
        {
            var result = await QueryAsync<Shelter>(_qs.Shelter.GetById, new { ShelterId = id });
            return result.FirstOrDefault();
        }

        public async Task<Shelter> Insert(Shelter shelter)
        {
            var result = await QueryAsync<Shelter>(_qs.Shelter.Insert, shelter );
            return result.FirstOrDefault();
        }

        public async Task<Shelter> Update(Shelter shelter)
        {
            var result = await QueryAsync<Shelter>(_qs.Shelter.Update, shelter);
            return result.FirstOrDefault();
        }

        public async Task Delete(Guid id)
        {
            await ExecuteAsync(_qs.Shelter.Delete, new { @ShelterId = id });
        }
    }
}
