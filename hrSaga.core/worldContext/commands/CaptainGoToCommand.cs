using hrSaga.core.infra;
using hrSaga.core.worldContext.enums;

namespace hrSaga.core.worldContext.commands
{
    public class CaptainGoToCommand : ICommand
    {
        public Location Location { get; }

        public CaptainGoToCommand(Location location)
        {
            Location = location;
        }
    }
}
