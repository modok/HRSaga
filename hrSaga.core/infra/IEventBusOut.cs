namespace hrSaga.core.infra
{
    public interface IEventBusOut
    {
        void PushEvent(IEvent e);
    }
}
