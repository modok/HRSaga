using System;
using hrSaga.core.infra;
using hrSaga.core.worldContext.commands;
using hrSaga.core.worldContext.entities;
using hrSaga.core.worldContext.enums;
using hrSaga.core.worldContext.events;

namespace hrSaga.core.worldContext
{
    public class WorldCommandHandler : ICommandHandler
    {
        public void Init(ICommandBusIn commandBus, IEventBusOut eventBus, DataStore dataStore)
        {
            commandBus.RegisterToCommand<CreateCaptainCommand>(c =>
            {
                var id = Guid.NewGuid();
                dataStore.Insert(new Captain(id) { Location = Location.Roaming });
                eventBus.PushEvent(new CaptainCreatedEvent(id));
            });

            commandBus.RegisterToCommand<CaptainGoToCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                var captainLocation = captain.Location;
                if (captainLocation != c.Location)
                {
                    captain.Location = c.Location;
                    dataStore.Update(captain);
                    eventBus.PushEvent(new CaptainMovedEvent(captainLocation, c.Location));
                }
                else
                {
                    throw new Exception("The captain is already there");
                }
            });
        }
    }
}
