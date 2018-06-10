using System;
using System.Text.RegularExpressions;
using hrSaga.core.infra;

namespace hrSaga.console.inputs
{
    public abstract class BaseInput1Args : IInput
    {
        public abstract ICommand Command { get; }

        protected String Argument { get; set; }

        public virtual void Init(string args)
        {
            var arg = args.Trim();
            if (!Regex.IsMatch(arg, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                throw new ArgumentException("Only 1 argument allowed");
            }

            Argument = arg;
        }
    }
}
