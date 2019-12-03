using BooksBorrow.Database.Context;
using BooksBorrow.Domain.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Database.POCO.DataAccess.Domain
{
    public partial class DomainDbDataAccessor : DbDataAccessorBase, IDomainDataAccessor
    {
        public DatabaseContext GetDataContext()
        {
            return this.DbContext;
        }
    }
}
