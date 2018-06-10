using hrSaga.core.infra;
using hrSaga.core.tavernContext.commands;

namespace hrSaga.core.tavernContext
{
    public class TavernEventHandler : IEventHandler
    {
        public void Init(IEventBusIn eventBus, ICommandBusOut commandBus)
        {
            eventBus.RegisterToEvent<worldContext.events.CaptainCreatedEvent>(e =>
            {
                commandBus.PushCommand(new CreateCaptainCommand(e.Id));
            });

            eventBus.RegisterToEvent<roamingContext.events.SquadMemberHiredEvent>(e =>
            {
                commandBus.PushCommand(new HireSquadMemberCommand());
            });

            eventBus.RegisterToEvent<worldContext.events.CaptainMovedEvent>(e =>
            {
                if (e.From == worldContext.enums.Location.Tavern
                    || e.To == worldContext.enums.Location.Tavern)
                {
                    var isValid = e.To == worldContext.enums.Location.Tavern;
                    commandBus.PushCommand(new UpdateIsValidCommand(isValid));
                }
            });
        }
    }
}
