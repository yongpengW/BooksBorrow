using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainModels.Identities._User_
{
    public class UserID: IdentityBase
    {
        public UserID(Guid id) : base(id)
        {
        }
    }
}
