using BooksBorrow.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Interface
{
    public partial interface IDomainDataAccessor : IDisposable
    {
        DatabaseContext GetDataContext();
    }
}
