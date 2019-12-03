using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Exceptions
{
    public class UniqueKeyConflictException : System.Exception
    {
        public const int SqlServerExceptionNumber = 2627;

        public UniqueKeyConflictException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
