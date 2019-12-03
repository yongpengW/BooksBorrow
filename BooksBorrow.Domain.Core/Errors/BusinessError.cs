using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Errors
{
    public class BusinessError
    {
        public ErrorCode ErrorCode
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public BusinessError(ErrorCode errorCode, string message)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }
    }
}
