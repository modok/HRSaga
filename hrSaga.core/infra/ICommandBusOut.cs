namespace hrSaga.core.infra
{
    public interface ICommandBusOut
    {
        void PushCommand(ICommand c);
    }
}
