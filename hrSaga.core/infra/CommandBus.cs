using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hrSaga.core.infra
{
    public class CommandBus : ICommandBusIn, ICommandBusOut
    {
        readonly Dictionary<Type, Action<ICommand>> _registry
            = new Dictionary<Type, Action<ICommand>>();

        public void RegisterToCommand<C>(Action<C> commandHandler)
            where C : class, ICommand
        {
            var commandType = typeof(C);
            _registry.Add(commandType, c => commandHandler(c as C));
        }

        public void PushCommand(ICommand c)
        {
            WriteLog(c);
            var commandType = c.GetType();
            if (_registry.ContainsKey(commandType))
            {
                var commandHandler = _registry[commandType];
                commandHandler(c);
            }
        }

        void WriteLog(ICommand c)
        {
            var json = JsonConvert.SerializeObject(c);
            Console.WriteLine($"Pushed command {c}: {json}");
        }
    }
}
