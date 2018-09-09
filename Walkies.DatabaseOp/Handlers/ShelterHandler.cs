using System;
using System.Collections.Generic;
using System.Text;

namespace Walkies.DatabaseOperations.Handlers
{
    using Walkies.Common.Models;
    using MediatR;
    using System.Threading.Tasks;
    using RT.Comb;
    using System.Linq;

    public partial class ShelterHandler
    {
        public class AddCmd : Shelter, IRequest<Shelter>
        {
        }

        public class AddHandler : AsyncRequestHandler<AddCmd, Shelter>
        {
            protected readonly ShelterRepository _repo;
            protected ICombProvider _guidProvider;
            public AddHandler(ShelterRepository repo, ICombProvider guidProvider)
            {
                _repo = repo;
                _guidProvider = guidProvider;
            }

            protected override async Task<Shelter> HandleCore(AddCmd cmd)
            {
                cmd.ShelterId = _guidProvider.Create();
                var result = await _repo.QueryAsync<Shelter>(_repo.Queries.Shelter.Insert, cmd);
                return result.FirstOrDefault();
            }
        }

        public class UpdateCmd : Shelter, IRequest
        {
        }

        public class UpdateHandler : AsyncRequestHandler<UpdateCmd>
        {
            protected readonly ShelterRepository _repo;
            public UpdateHandler(ShelterRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(UpdateCmd cmd)
            {
                await _repo.Update(cmd);
            }
        }

        public class DeleteCmd : Shelter, IRequest { }
        public class DeleteHandler : AsyncRequestHandler<DeleteCmd>
        {
            protected readonly RdbmsRepository _repo;
            public DeleteHandler(RdbmsRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(DeleteCmd cmd)
            {
                await _repo.ExecuteAsync(_repo.Queries.Shelter.Delete, cmd);
            }
        }
    }
}
