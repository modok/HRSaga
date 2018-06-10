using hrSaga.core.infra;

namespace hrSaga.core.tavernContext.commands
{
    public class UpdateIsValidCommand : ICommand
    {
        public bool IsValid { get; }

        public UpdateIsValidCommand(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
