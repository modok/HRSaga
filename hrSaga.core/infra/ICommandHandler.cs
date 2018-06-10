namespace hrSaga.core.infra
{
    public interface ICommandHandler
    {
        void Init(ICommandBusIn commandBus, IEventBusOut eventBus, DataStore dataStore);
    }
}
