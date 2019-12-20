
using System;
using System.Text;
using CQRSlite.Domain;
using CQRSlite.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace HRSaga.Adventure.Common.EventStore.Events
{
    public abstract class EVBaseEvent : IEvent, IEVEvent
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public EVBaseEvent(String aggregateName,Identity aggregateIdentity): this(Guid.NewGuid(),aggregateName,aggregateIdentity){}

        public EVBaseEvent(Guid eventId,String aggregateName,Identity aggregateIdentity){
            this.EventId = eventId;
            this.AggregateName = aggregateName;
            this.Identity = aggregateIdentity;
        }

        public String AggregateName {get;  set;}
        public Guid EventId {get;  set;}
        public  Identity Identity { get; set; }
        public  int Version { get; set; }
        public  DateTimeOffset TimeStamp { get; set; }

        

        public EventData toEventData(bool json = true)
        {
            String data=JsonConvert.SerializeObject(this,JsonSerializerSettings);

            return new EventData(this.EventId, $"{this.AggregateName}:{this.Identity}", json, Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(""));
        }
    }
}