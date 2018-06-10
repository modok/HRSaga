namespace hrSaga.core.infra
{
    public class BoundedContext<CH, EH>
        where CH : ICommandHandler, new()
        where EH : IEventHandler, new()
    {
        readonly CH _commandHandler;
        readonly EH _eventHandler;
        readonly DataStore _dataStore;

        public BoundedContext(CommandBus commandBus, EventBus eventBus)
        {
            _commandHandler = new CH();
            _eventHandler = new EH();
            _dataStore = new DataStore();

            _commandHandler.Init(commandBus, eventBus, _dataStore);
            _eventHandler.Init(eventBus, commandBus);
        }
    }
}
