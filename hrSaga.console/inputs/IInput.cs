using hrSaga.core.infra;

namespace hrSaga.console.inputs
{
    public interface IInput
    {
        ICommand Command { get; }

        void Init(string args);
    }
}
