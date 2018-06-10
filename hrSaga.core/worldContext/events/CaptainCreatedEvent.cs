using System;
using hrSaga.core.infra;

namespace hrSaga.core.worldContext.events
{
    public class CaptainCreatedEvent : IEvent
    {
        public Guid Id { get; }

        public CaptainCreatedEvent(Guid id)
        {
            Id = id;
        }
    }
}
