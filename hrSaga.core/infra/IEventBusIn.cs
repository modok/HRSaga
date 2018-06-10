using System;

namespace hrSaga.core.infra
{
    public interface IEventBusIn
    {
        void RegisterToEvent<E>(Action<E> eventHandler)
            where E : class, IEvent;
    }
}
