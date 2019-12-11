
using System;
using CQRSlite.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CaptainCreated : IEvent
    {

        public CaptainCreated(CaptainId captainId)
        {
            this.Id = captainId.Id;
            System.Console.WriteLine("Event: CaptainCreated");
        }

        public Guid Id { get; set;}
        public int Version { get; set;}
        public DateTimeOffset TimeStamp { get; set;}

        public CaptainId captainId(){
            return new CaptainId(this.Id);
        }
    }
}