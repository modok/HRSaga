using System;
using hrSaga.core.infra;
using hrSaga.core.roamingContext.commands;
using hrSaga.core.roamingContext.entities;
using hrSaga.core.roamingContext.events;

namespace hrSaga.core.roamingContext
{
    public class RoamingCommandHandler : ICommandHandler
    {
        public void Init(ICommandBusIn commandBus, IEventBusOut eventBus, DataStore dataStore)
        {
            commandBus.RegisterToCommand<CreateCaptainCommand>(c =>
            {
                dataStore.Insert(new Captain(c.Id) { IsValid = true });
                eventBus.PushEvent(new CaptainCreatedEvent(c.Id));
            });

            commandBus.RegisterToCommand<HireSquadMemberCommand>(c =>
            {
                const int MAX_SQUAD_SIZE = 5;

                var captain = dataStore.GetFirst<Captain>();
                if (captain.IsValid)
                {
                    if (captain.SquadSize < MAX_SQUAD_SIZE)
                    {
                        captain.SquadSize++;
                        dataStore.Update(captain);
                        eventBus.PushEvent(new SquadMemberHiredEvent());
                    }
                    else
                    {
                        throw new Exception("The squad size has reached its limit");
                    }
                }
                else
                {
                    throw new Exception("The captain is not in roaming");
                }
            });

            commandBus.RegisterToCommand<UpdateIsValidCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                captain.IsValid = c.IsValid;
                dataStore.Update(captain);
                eventBus.PushEvent(new IsValidUpdatedEvent(c.IsValid));
            });
        }
    }
}
