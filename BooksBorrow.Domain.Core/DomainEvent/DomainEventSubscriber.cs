using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainEvent
{
    internal class DomainEventSubscriber
    {
        public Type EventType { get; private set; }

        public bool IsAsync { get; private set; }

        public Delegate EventHandler { get; private set; }

        public DomainEventSubscriber(Type eventType, Delegate handler, bool isAsync = false)
        {
            this.EventType = eventType;
            this.EventHandler = handler;
            this.IsAsync = isAsync;
        }
    }
}
