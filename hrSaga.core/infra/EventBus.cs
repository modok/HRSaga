using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hrSaga.core.infra
{
    public class EventBus : IEventBusIn, IEventBusOut
    {
        readonly Dictionary<Type, List<Action<IEvent>>> _registry
            = new Dictionary<Type, List<Action<IEvent>>>();

        public void RegisterToEvent<E>(Action<E> eventHandler)
            where E : class, IEvent
        {
            var eventType = typeof(E);
            if (!_registry.ContainsKey(eventType))
            {
                _registry.Add(eventType, new List<Action<IEvent>>());
            }

            _registry[eventType].Add(e => eventHandler(e as E));
        }

        public void PushEvent(IEvent e)
        {
            WriteLog(e);
            var eventType = e.GetType();
            if (_registry.ContainsKey(eventType))
            {
                _registry[eventType].ForEach(eventHandler =>
                                             eventHandler(e));
            }
        }

        void WriteLog(IEvent e)
        {
            var json = JsonConvert.SerializeObject(e);
            Console.WriteLine($"Pushed event {e}: {json}");
        }
    }
}
