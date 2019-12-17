using System;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CharacterHired : IEvent
    {
        public ICharacter Character { get; private set; }

        public CharacterHired(CaptainId captainId, ICharacter character)
        {
            this.Identity = captainId;
            this.Character=character;
        }
        public CaptainId captainId(){
            return (CaptainId)Identity;
        } 

        public Identity Identity { get; set;}
        public int Version { get; set;}
        public DateTimeOffset TimeStamp { get; set;}
    }
}