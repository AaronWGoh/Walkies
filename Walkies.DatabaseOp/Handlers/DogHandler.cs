using System;
using System.Collections.Generic;
using System.Text;

namespace Walkies.DatabaseOperations.Handlers
{
    using Walkies.Common;
    using Walkies.Common.Models;
    using MediatR;
    using System.Threading.Tasks;
    using RT.Comb;
    using System.Linq;

    public partial class DogHandler
    {
        public class AddCmd : Dog, IRequest<Dog>
        {
        }

        public class AddHandler : AsyncRequestHandler<AddCmd, Dog>
        {
            protected readonly DogRepository _repo;
            protected ICombProvider _guidProvider;
            public AddHandler(DogRepository repo, ICombProvider guidProvider)
            {
                _repo = repo;
                _guidProvider = guidProvider;
            }

            protected override async Task<Dog> HandleCore(AddCmd cmd)
            {
                cmd.DogId = _guidProvider.Create();
                var result = await _repo.QueryAsync<Dog>(_repo.Queries.Dog.Insert, cmd);
                return result.FirstOrDefault();

            }
        }

        public class UpdateCmd : Dog, IRequest
        {
        }

        public class UpdateHandler : AsyncRequestHandler<UpdateCmd>
        {
            protected readonly DogRepository _repo;
            public UpdateHandler(DogRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(UpdateCmd cmd)
            {
                await _repo.Update(cmd);
            }
        }

        public class DeleteCmd : Dog, IRequest { }
        public class DeleteHandler : AsyncRequestHandler<DeleteCmd>
        {
            protected readonly RdbmsRepository _repo;
            public DeleteHandler(RdbmsRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(DeleteCmd cmd)
            {
                await _repo.ExecuteAsync(_repo.Queries.Dog.Delete, cmd);
            }
        }
    }
}
