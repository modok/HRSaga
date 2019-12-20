
using EventFlow.Aggregates;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class CaptainCreated : AggregateEvent<Captain, CaptainId>
    {
        public string name {get;}
        public CaptainCreated(string name){
            this.name = name;
        }
    }
}