using hrSaga.core.infra;

namespace hrSaga.core.tavernContext.events
{
    public class IsValidUpdatedEvent : IEvent
    {
        public bool IsValid { get; }

        public IsValidUpdatedEvent(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
