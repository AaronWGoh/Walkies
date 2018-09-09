using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkies.Common;
using Walkies.Common.Models;

namespace Walkies.DatabaseOperations
{
    public class DogRepository : RdbmsRepository, IRepository
    {
        public DogRepository(IConfiguration cfg, Queries qs) : base(cfg, qs) { }

        public async Task<IEnumerable<Dog>> GetAll()
        {
            return await QueryAsync<Dog>(_qs.Dog.GetAll);
        }

        public async Task<Dog> GetById(Guid id)
        {
            var result = await QueryAsync<Dog>(_qs.Dog.GetById, new { DogId = id });
            return result.FirstOrDefault();
        }

        public async Task<Dog> Insert(Dog dog)
        {
            var result = await QueryAsync<Dog>(_qs.Dog.Insert, dog );
            return result.FirstOrDefault();
        }

        public async Task<Dog> Update(Dog dog)
        {
            var result = await QueryAsync<Dog>(_qs.Dog.Update, dog );
            return result.FirstOrDefault();
        }

        public async Task Delete(Guid id)
        {
            await ExecuteAsync(_qs.Dog.Delete, new { @DogId = id });
        }
    }
}
