using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Domain;
using CQRSlite.Events;
using EventStore.ClientAPI;
using HRSaga.Adventure.Common.EventStore.Clients;
using HRSaga.Adventure.Common.EventStore.Events;
using Newtonsoft.Json;

namespace HRSaga.Adventure.Common.EventStore.EventStores
{
    public class EVEventStore : IEventStore
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private readonly IEventPublisher _publisher;
        private readonly EVClient _EVClient;
        private readonly String _streamName;

        public EVEventStore(IEventPublisher publisher,EVClient eventStoreClient, String streamName)
        {
            _publisher = publisher;
            _EVClient = eventStoreClient;
            _streamName = streamName;

        }
        public async Task<IEnumerable<IEvent>> Get(Identity aggregateIdentity, int fromVersion, CancellationToken cancellationToken = default)
        {
            List<ResolvedEvent> resolvedEvents=await _EVClient.ReadEvent(_streamName).ConfigureAwait(false);

            var aggregateEvents = resolvedEvents.Where(commit => commit.Event.EventType.Contains($":{aggregateIdentity.Id.ToString()}:"));
            
           var events = aggregateEvents.Select(serializedEvent => {
                    String json=Encoding.UTF8.GetString(serializedEvent.Event.Data);
                    return JsonConvert.DeserializeObject<IEvent>(json,JsonSerializerSettings);
                    }).ToList();
            
            return events;
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        {
            foreach (var @event in events)
            {
                if(!(@event is EVBaseEvent)){
                    throw new Exception("Wrong type");
                }
                EventData evetData=((EVBaseEvent)@event).toEventData(true);
                _EVClient.AddEvent(evetData,_streamName);
                
                await _publisher.Publish(@event, cancellationToken);
            }
            
        }

        
    }
}
