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

    public partial class AccountUserHandler
    {
        public class AddCmd : AccountUser, IRequest<AccountUser>
        {
        }

        public class AddHandler : AsyncRequestHandler<AddCmd, AccountUser>
        {
            protected readonly AccountUserRepository _repo;
            protected ICombProvider _guidProvider;
            public AddHandler(AccountUserRepository repo, ICombProvider guidProvider)
            {
                _repo = repo;
                _guidProvider = guidProvider;
            }

            protected override async Task<AccountUser> HandleCore(AddCmd cmd)
            {
                cmd.AccountUserId = _guidProvider.Create();
                var result = await _repo.QueryAsync<AccountUser>(_repo.Queries.Dog.Insert, cmd);
                return result.FirstOrDefault();

            }
        }

        public class UpdateCmd : AccountUser, IRequest
        {
        }

        public class UpdateHandler : AsyncRequestHandler<UpdateCmd>
        {
            protected readonly AccountUserRepository _repo;
            public UpdateHandler(AccountUserRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(UpdateCmd cmd)
            {
                await _repo.Update(cmd);
            }
        }

        public class DeleteCmd : AccountUser, IRequest { }
        public class DeleteHandler : AsyncRequestHandler<DeleteCmd>
        {
            protected readonly RdbmsRepository _repo;
            public DeleteHandler(RdbmsRepository repo)
            {
                _repo = repo;
            }

            protected override async Task HandleCore(DeleteCmd cmd)
            {
                await _repo.ExecuteAsync(_repo.Queries.AccountUser.Delete, cmd);
            }
        }
    }
}
