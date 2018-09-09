using System;
using System.Collections.Generic;
using System.Text;
using Walkies.Common;

namespace Walkies.DatabaseOperations
{
    public class UserTypeQueries
    {
        public string GetAll
        {
            get
            {
                return "select * from UserType";
            }
        }
    }

    public partial class Queries : IQueries
    {
        public UserTypeQueries UserType = new UserTypeQueries();
    }
}
