
using System;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace HRSaga.Adventure.Context.Common.Domain.Model
{
    public class BaseEvent : IEvent
    {

        public Identity Identity { get; set ; }
        public int Version { get; set ; }
        public DateTimeOffset TimeStamp { get; set ; }
    }
}