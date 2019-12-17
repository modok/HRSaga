using System;

namespace CQRSlite.Domain.Exception
{
    public class EventIdIncorrectException : System.Exception
    {
        public EventIdIncorrectException( Identity id, Identity aggregateId)
            : base($"Event {id} has a different Id from it's aggregates Id ({aggregateId})")
        { }
    }
}
