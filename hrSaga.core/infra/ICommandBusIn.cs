using System;

namespace hrSaga.core.infra
{
    public interface ICommandBusIn
    {
        void RegisterToCommand<C>(Action<C> commandHandler)
            where C : class, ICommand;
    }
}
