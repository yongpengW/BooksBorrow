using BooksBorrow.Domain.Core.Commands;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainEvent
{
    public class DomainEventPublisher
    {
        private static Dictionary<Type, Dictionary<string, DomainEventSubscriber>> Subscribers { get; set; }

        private static object lockObject;

        static DomainEventPublisher()
        {
            Subscribers = new Dictionary<Type, Dictionary<string, DomainEventSubscriber>>();
            lockObject = new Object();
        }

        public static void Publish<T>(T domainEvent) where T : Events.DomainEvent
        {
            if (Subscribers.Any())
            {
                Type eventType = domainEvent.GetType();

                if (Subscribers.ContainsKey(eventType))
                {
                    var registeredSubscribers = Subscribers[eventType].Select(r => r.Value).Where(s => s.EventType == eventType);

                    foreach (var s in registeredSubscribers)
                    {
                        Action<T> action = (Action<T>)s.EventHandler;

                        if (s.IsAsync)
                        {
                            action.BeginInvoke(domainEvent, callback =>
                            {
                                action.EndInvoke(callback);
                            }, null);
                        }
                        else
                        {
                            action.Invoke(domainEvent);
                        }
                    }
                }
            }
        }

        public static void Subscribe<T>(string key, Action<T> handler) where T : Events.DomainEvent
        {
            Subscribe(key, new DomainEventSubscriber(typeof(T), handler, false));
        }

        public static void SubscribeAsync<T>(string key, Action<T> handler) where T : Events.DomainEvent
        {
            Subscribe(key, new DomainEventSubscriber(typeof(T), handler, true));
        }

        private static void Subscribe(string key, DomainEventSubscriber domainEventSubscriber)
        {
            var eventType = domainEventSubscriber.EventType;

            if (!Subscribers.ContainsKey(eventType))
            {
                lock (lockObject)
                {
                    if (!Subscribers.ContainsKey(eventType))
                    {
                        Subscribers.Add(eventType, new Dictionary<string, DomainEventSubscriber>());
                    }
                }
            }

            var eventSubscribers = Subscribers[eventType];

            if (!eventSubscribers.ContainsKey(key))
            {
                lock (lockObject)
                {
                    if (!eventSubscribers.ContainsKey(key))
                    {
                        eventSubscribers.Add(key, domainEventSubscriber);
                    }
                }
            }
        }

        public static void Reset()
        {
            Subscribers.Clear();
        }

        public static void Reset<T>()
        {
            Type eventType = typeof(T);

            if (Subscribers.ContainsKey(eventType))
            {
                Subscribers.Remove(eventType);
            }
        }

        public static void Reset<T>(string key) where T : Events.DomainEvent
        {
            if (!String.IsNullOrWhiteSpace(key))
            {
                Type eventType = typeof(T);

                if (Subscribers.ContainsKey(eventType))
                {
                    Subscribers[eventType].Remove(key);

                    if (!Subscribers[eventType].Any())
                    {
                        Reset<T>();
                    }
                }
            }
        }

        private static void LogError(Events.DomainEvent domainEvent, System.Exception exception)
        {
            var errorLogCommand = new AddErrorLogCommand();

            errorLogCommand.ErrorObject = domainEvent;
            errorLogCommand.Exception = exception;

            InjectContainer.GetInstance<ICommandBus>().Send(errorLogCommand);
        }
    }
}
