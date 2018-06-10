namespace hrSaga.core.infra
{
    public interface IEventHandler
    {
        void Init(IEventBusIn eventBus, ICommandBusOut commandBus);
    }
}
