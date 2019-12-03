using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Database.POCO.DataAccess.Query
{
    public partial class QueryDbDataAccessor : QueryDbDataAccessorBase
    {
        public QueryDbDataAccessor(string connectionString) : base(connectionString)
        {
        }
    }
}
