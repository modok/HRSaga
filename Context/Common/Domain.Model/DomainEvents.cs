using System;
using System.Collections.Generic;

namespace HRSaga.Context.Common.Domain.Model
{
    public static class DomainEvents
    {
        private static Dictionary<Type, List<Delegate>> _handlers;

        public static void Register<T>(Action<T> eventHandler)
            where T : IDomainEvent
        {
            _handlers[typeof(T)].Add(eventHandler);
        }

        public static void Raise<T>(T domainEvent)
            where T : IDomainEvent
        {
            foreach (Delegate handler in _handlers[domainEvent.GetType()])
            {
                var action = (Action<T>)handler;
                action(domainEvent);
            }
        }
    }
}