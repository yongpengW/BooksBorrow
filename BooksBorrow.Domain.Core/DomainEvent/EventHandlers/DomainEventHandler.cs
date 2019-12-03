using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainEvent.EventHandlers
{
    public abstract class DomainEventHandler<T> where T : Events.DomainEvent
    {
        public string EventKey { get; private set; }

        public DomainEventHandler()
        {
            EventKey = typeof(T).FullName;
        }

        protected abstract Action<T> GetHandlerFunction();

        public void Subscribe()
        {
            Action<T> handlerFunction = GetHandlerFunction();

            DomainEventPublisher.Subscribe(EventKey, handlerFunction);
        }
    }
}
