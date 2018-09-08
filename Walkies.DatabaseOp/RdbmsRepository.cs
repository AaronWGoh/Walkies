using System;
using System.Collections.Generic;
using System.Text;

namespace Walkies.Common
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    using Microsoft.Extensions.Configuration;

    using Dapper;
    using Walkies.Common;
    using Walkies.DatabaseOperations;

    public class RdbmsRepository : Database, IRepository
    {
        public RdbmsRepository(IConfiguration cfg, Queries qs) : base(cfg) { _qs = qs; }

        protected Queries _qs;
        public Queries Queries { get { return _qs; } }
    }
}
