using hrSaga.core.infra;
using hrSaga.core.roamingContext.commands;

namespace hrSaga.core.roamingContext
{
    public class RoamingEventHandler : IEventHandler
    {
        public void Init(IEventBusIn eventBus, ICommandBusOut commandBus)
        {
            eventBus.RegisterToEvent<worldContext.events.CaptainCreatedEvent>(e =>
            {
                commandBus.PushCommand(new CreateCaptainCommand(e.Id));
            });

            eventBus.RegisterToEvent<worldContext.events.CaptainMovedEvent>(e =>
            {
                if (e.From == worldContext.enums.Location.Roaming
                   || e.To == worldContext.enums.Location.Roaming)
                {
                    var isValid = e.To == worldContext.enums.Location.Roaming;
                    commandBus.PushCommand(new UpdateIsValidCommand(isValid));
                }
            });
        }
    }
}
