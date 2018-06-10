using hrSaga.core.infra;
using hrSaga.core.worldContext.commands;

namespace hrSaga.console.inputs
{
    public class CreateCaptainInput : BaseInput0Args
    {
        public override ICommand Command => new CreateCaptainCommand();
    }
}
