using System;
using System.Collections.Generic;
using hrSaga.core.infra;
using hrSaga.core.worldContext.commands;
using hrSaga.core.worldContext.enums;

namespace hrSaga.console.inputs
{
    public class GoToInput : BaseInput1Args
    {
        public override ICommand Command
        {
            get
            {
                switch (Argument.ToLower())
                {
                    case "roaming": return new CaptainGoToCommand(Location.Roaming);
                    case "tavern": return new CaptainGoToCommand(Location.Tavern);
                    default: return null;
                }
            }
        }

        public override void Init(string args)
        {
            base.Init(args);
            if (!(new List<string> { "roaming", "tavern" }.Contains(Argument.ToLower())))
            {
                throw new ArgumentException("Can only go to Roaming or Tavern");
            }
        }
    }
}
