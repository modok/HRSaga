using EventFlow.Aggregates;
using EventFlow.ReadStores;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.ReadModels
{
    public class CaptainReadModel : IReadModel,
        IAmReadModelFor<Captain, CaptainId, CaptainCreated>,
        IAmReadModelFor<Captain, CaptainId, WarriorHired>,
        IAmReadModelFor<Captain, CaptainId, WizardHired>
    {
        public CaptainId captainId {get; private set;}
        public int warriors {get; private set;}
        public int wizards {get; private set;}
        public void Apply(IReadModelContext context, IDomainEvent<Captain, CaptainId, CaptainCreated> domainEvent)
        {
            captainId = domainEvent.AggregateIdentity;
            warriors = 0;
            wizards = 0;
        }
        public void Apply(IReadModelContext context, IDomainEvent<Captain, CaptainId, WizardHired> domainEvent)
        {
            wizards++;
        }
        public void Apply(IReadModelContext context, IDomainEvent<Captain, CaptainId, WarriorHired> domainEvent)
        {
            warriors++;
        }
    }

}