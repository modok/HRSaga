using System;
using System.Collections.Generic;
using hrSaga.core.infra;
using hrSaga.core.roamingContext.commands;

namespace hrSaga.console.inputs
{
    public class HireSquadMemberInput : BaseInput1Args
    {
        public override ICommand Command => new HireSquadMemberCommand();

        public override void Init(string args)
        {
            base.Init(args);
            if (!(new List<string> { "warrior", "wizard" }.Contains(Argument.ToLower())))
            {
                throw new ArgumentException("Can only hire Warriors or Wizards");
            }
        }
    }
}
