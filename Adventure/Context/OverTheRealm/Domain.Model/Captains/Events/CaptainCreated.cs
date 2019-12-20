
using System;
using HRSaga.Adventure.Common.EventStore.Events;
using Newtonsoft.Json;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CaptainCreated : EVBaseEvent
    {

        public CaptainCreated(CaptainId captainId):base("Captain", captainId){}

        [JsonConstructor]
        public CaptainCreated(CaptainId captainId,Guid eventId,String eventType):base(eventId,"Captain", captainId){}

        public CaptainId captainId(){
            return (CaptainId)Identity;
        }

    }
}