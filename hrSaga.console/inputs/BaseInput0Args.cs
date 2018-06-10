using System;
using hrSaga.core.infra;

namespace hrSaga.console.inputs
{
    public abstract class BaseInput0Args : IInput
    {
        public abstract ICommand Command { get; }

        public void Init(string args)
        {
            if (args.Trim() != string.Empty)
            {
                throw new ArgumentException("No arguments allowed");
            }
        }
    }
}
