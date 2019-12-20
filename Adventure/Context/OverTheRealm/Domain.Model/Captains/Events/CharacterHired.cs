using System;
using HRSaga.Adventure.Common.EventStore.Events;
using Newtonsoft.Json;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CharacterHired : EVBaseEvent
    {
        public ICharacter Character { get; private set; }

        public CharacterHired(CaptainId captainId, ICharacter character):base("Captain", captainId)
        {
            this.Character=character;
        }

        [JsonConstructor]
        public CharacterHired(CaptainId captainId,ICharacter character, Guid eventId, String eventType):base(eventId, "Captain", captainId){
            this.Character=character;
        }
        public CaptainId captainId(){
            return (CaptainId)Identity;
        } 

    }
}