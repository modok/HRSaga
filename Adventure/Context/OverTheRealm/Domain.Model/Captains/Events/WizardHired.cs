using EventFlow.Aggregates;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events
{
    public class WizardHired : AggregateEvent<Captain, CaptainId>
    {
        public Wizard Wizard { get;}

        public WizardHired(Wizard wizard)
        {
            this.Wizard=wizard;
        } 
    }
}