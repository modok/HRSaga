using System;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CaptainSquadFullFilled : IEvent
    {
        public CaptainSquadFullFilled(CaptainId captainId, int squadSize)
        {
            this.Identity = captainId;
            this.SquadSize = squadSize;
        }
        public CaptainId captainId(){
            return (CaptainId)(Identity);
        } 

        public int SquadSize { get; set;}
        public Identity Identity { get; set;}
        public int Version { get; set;}
        public DateTimeOffset TimeStamp { get; set;}
    }
}