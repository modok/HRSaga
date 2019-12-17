using System;

namespace CQRSlite.Domain.Exception
{
    public class AggregateNotFoundException : System.Exception
    {
        public AggregateNotFoundException(Type t, Identity identity)
            : base($"Aggregate {identity} of type {t.FullName} was not found")
        { }
    }
}
