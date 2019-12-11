using System;
using CQRSlite.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CharacterHired : IEvent
    {
        public ICharacter Character { get; private set; }

        public CharacterHired(CaptainId aggregateId, ICharacter character)
        {
            this.Id = aggregateId.Id;
            this.Character=character;
        }
        public CaptainId captainId(){
            return new CaptainId(Id);
        } 

        public Guid Id { get; set;}
        public int Version { get; set;}
        public DateTimeOffset TimeStamp { get; set;}
    }
}