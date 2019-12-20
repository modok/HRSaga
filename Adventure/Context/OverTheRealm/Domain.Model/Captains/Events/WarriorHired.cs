using EventFlow.Aggregates;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class WarriorHired : AggregateEvent<Captain, CaptainId>
    {
        public Warrior Warrior { get;}

        public WarriorHired(Warrior warrior)
        {
            this.Warrior=warrior;
        } 
    }
}