using System;

namespace CQRSlite.Domain.Exception
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(Identity identity)
            : base($"A different version than expected was found in aggregate {identity}")
        { }
    }
}