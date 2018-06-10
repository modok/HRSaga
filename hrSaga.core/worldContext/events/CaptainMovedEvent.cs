using hrSaga.core.infra;
using hrSaga.core.worldContext.enums;

namespace hrSaga.core.worldContext.events
{
    public class CaptainMovedEvent : IEvent
    {
        public Location From { get; }
        public Location To { get; }

        public CaptainMovedEvent(Location from, Location to)
        {
            From = from;
            To = to;
        }
    }
}
