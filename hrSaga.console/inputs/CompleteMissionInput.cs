using hrSaga.core.infra;
using hrSaga.core.tavernContext.commands;

namespace hrSaga.console.inputs
{
    public class CompleteMissionInput : BaseInput0Args
    {
        public override ICommand Command => new CompleteMissionCommand();
    }
}
