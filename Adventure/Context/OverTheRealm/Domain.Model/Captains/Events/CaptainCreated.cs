
using System.Collections.Generic;
using EventFlow.Aggregates;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CaptainCreated : AggregateEvent<Captain, CaptainId>
    {
        public List<ICharacter> Squad {get;}

        public CaptainCreated(List<ICharacter> squad){
            this.Squad = squad;
        }
    }
}