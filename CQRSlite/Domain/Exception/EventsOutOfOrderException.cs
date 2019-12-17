using System;

namespace CQRSlite.Domain.Exception
{
    public class EventsOutOfOrderException : System.Exception
    {
        public EventsOutOfOrderException(Identity identity)
            : base($"Eventstore gave events for aggregate {identity} out of order")
        { }
    }
}