using BooksBorrow.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainEvent.Events
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn
        {
            get;
            private set;
        }

        public DomainEvent()
        {
            this.OccurredOn = Utility.GetCurrentDateTime();
        }
    }
}
