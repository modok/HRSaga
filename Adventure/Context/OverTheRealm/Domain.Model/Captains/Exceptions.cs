using System;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class TooManyMembersException : Exception
    {
        public TooManyMembersException() { }
        public TooManyMembersException(string message) : base(message) { }
        public TooManyMembersException(string message, System.Exception inner) : base(message, inner) { }
        protected TooManyMembersException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}