using System;
using hrSaga.core.infra;
using hrSaga.core.roamingContext;
using hrSaga.core.tavernContext;
using hrSaga.core.worldContext;

namespace hrSaga.core
{
    public class HRSaga
    {
        readonly EventBus _eventBus = new EventBus();
        readonly CommandBus _commandBus = new CommandBus();

        readonly BoundedContext<WorldCommandHandler, WorldEventHandler> _locationContext;
        readonly BoundedContext<RoamingCommandHandler, RoamingEventHandler> _roamingContext;
        readonly BoundedContext<TavernCommandHandler, TavernEventHandler> _tavernContext;

        public HRSaga()
        {
            _locationContext = new BoundedContext<WorldCommandHandler, WorldEventHandler>(_commandBus, _eventBus);
            _roamingContext = new BoundedContext<RoamingCommandHandler, RoamingEventHandler>(_commandBus, _eventBus);
            _tavernContext = new BoundedContext<TavernCommandHandler, TavernEventHandler>(_commandBus, _eventBus);
        }

        public void PushCommand(ICommand c)
        {
            try
            {
                _commandBus.PushCommand(c);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Thrown error: {e.Message}");
            }
        }
    }
}
