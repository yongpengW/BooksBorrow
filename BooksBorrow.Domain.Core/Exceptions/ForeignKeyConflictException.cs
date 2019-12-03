using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Exceptions
{
    public class ForeignKeyConflictException : System.Exception
    {
        public const int SqlServerExceptionNumber = 547;

        public ForeignKeyConflictException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
