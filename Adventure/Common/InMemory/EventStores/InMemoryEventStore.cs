using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace HRSaga.Adventure.Common.InMemory.EventStores
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly Dictionary<Identity, List<IEvent>> _inMemoryDb = new Dictionary<Identity, List<IEvent>>();

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        {
            System.Console.WriteLine("EventStore: Event Saved");
            foreach (var @event in events)
            {
                _inMemoryDb.TryGetValue(@event.Identity, out var list);
                if (list == null)
                {
                    list = new List<IEvent>();
                    _inMemoryDb.Add(@event.Identity, list);
                }
                list.Add(@event);
                await _publisher.Publish(@event, cancellationToken);
            }
        }

        public Task<IEnumerable<IEvent>> Get(Identity aggregateIdentity, int fromVersion, CancellationToken cancellationToken = default)
        {
            _inMemoryDb.TryGetValue(aggregateIdentity, out var events);
            return Task.FromResult(events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>());
        }
    }
}
