using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Database.POCO.DataAccess
{
    public abstract class QueryDbDataAccessorBase : DbDataAccessorBase
    {
        public QueryDbDataAccessorBase(string connectionString) : base(connectionString)
        {
        }
    }
}
