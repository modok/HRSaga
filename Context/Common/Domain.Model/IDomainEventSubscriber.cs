namespace HRSaga.Context.Common.Domain.Model
{
    using System;

    public interface IDomainEventSubscriber<T> where T : IDomainEvent
    {
        void HandleEvent(T domainEvent);
        Type SubscribedToEventType();
    }
}
