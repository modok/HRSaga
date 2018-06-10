using hrSaga.core.infra;
using hrSaga.core.tavernContext.commands;

namespace hrSaga.console.inputs
{
    public class StartMissionInput : BaseInput0Args
    {
        public override ICommand Command => new StartMissionCommand();
    }
}
