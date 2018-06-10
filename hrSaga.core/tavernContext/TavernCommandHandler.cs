using System;
using hrSaga.core.infra;
using hrSaga.core.tavernContext.commands;
using hrSaga.core.tavernContext.entities;
using hrSaga.core.tavernContext.events;

namespace hrSaga.core.tavernContext
{
    public class TavernCommandHandler : ICommandHandler
    {
        public void Init(ICommandBusIn commandBus, IEventBusOut eventBus, DataStore dataStore)
        {
            commandBus.RegisterToCommand<CreateCaptainCommand>(c =>
            {
                dataStore.Insert(new Captain(c.Id)
                {
                    IsMissionSignedOff = false,
                    IsMissionStarted = false,
                    IsValid = false
                });
                eventBus.PushEvent(new CaptainCreatedEvent(c.Id));
            });

            commandBus.RegisterToCommand<HireSquadMemberCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                captain.SquadSize++;
                dataStore.Update(captain);
                eventBus.PushEvent(new SquadMemberHiredEvent());
            });

            commandBus.RegisterToCommand<UpdateIsValidCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                captain.IsValid = c.IsValid;
                dataStore.Update(captain);
                eventBus.PushEvent(new IsValidUpdatedEvent(c.IsValid));
            });

            commandBus.RegisterToCommand<SignOffMissionCommand>(c =>
            {
                const int MAX_SQUAD_SIZE = 5;

                var captain = dataStore.GetFirst<Captain>();
                if (captain.IsValid)
                {
                    if (!captain.IsMissionSignedOff)
                    {
                        if (captain.SquadSize == MAX_SQUAD_SIZE)
                        {
                            captain.IsMissionSignedOff = true;
                            dataStore.Update(captain);
                            eventBus.PushEvent(new MissionSignedOffEvent());
                        }
                        else
                        {
                            throw new Exception("The squad is not ready to start");
                        }
                    }
                    else
                    {
                        throw new Exception("A mission is already signed off");
                    }
                }
                else
                {
                    throw new Exception("The captain is not in the tavern");
                }
            });

            commandBus.RegisterToCommand<StartMissionCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                if (captain.IsValid)
                {
                    if (captain.IsMissionSignedOff)
                    {
                        captain.IsMissionStarted = true;
                        dataStore.Update(captain);
                        eventBus.PushEvent(new MissionStartedEvent());
                    }
                    else
                    {
                        throw new Exception("No mission has been signed off");
                    }
                }
                else
                {
                    throw new Exception("The captain is not in the tavern");
                }
            });

            commandBus.RegisterToCommand<CompleteMissionCommand>(c =>
            {
                var captain = dataStore.GetFirst<Captain>();
                if (captain.IsValid)
                {
                    if (captain.IsMissionStarted)
                    {
                        captain.IsMissionSignedOff = false;
                        captain.IsMissionStarted = false;
                        dataStore.Update(captain);
                        eventBus.PushEvent(new MissionCompletedEvent());
                    }
                    else
                    {
                        throw new Exception("No mission has been started");
                    }
                }
                else
                {
                    throw new Exception("The captain is not in the tavern");
                }
            });
        }
    }
}
