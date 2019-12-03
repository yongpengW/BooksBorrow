using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Commands
{
    public class AddErrorLogCommand: CommonCommand
    {
        public Object ErrorObject { get; set; }

        public Exception Exception { get; set; }
    }
}
