using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace HRSaga.Adventure.Common.EventStore.Clients
{

    public class EVClient : IEVClient
    {
        public IEventStoreConnection _connection { get; private set; }

        public EVClient()
        {
            Connect();
        }

        private void Connect()
        {
            this._connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            this._connection.ConnectAsync().Wait();
        }


        public async Task<List<ResolvedEvent>> ReadEvent(string streamName)
        {
            Connect();
            //var streamEvents = _connection.ReadStreamEventsForwardAsync(streamName, 0, 10, false).Result;

            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice =await _connection.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);

                nextSliceStart = Convert.ToInt32(currentSlice.NextEventNumber);

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            return streamEvents;
        }

        public void AddEvent(EventData eventData, string streamName)
        {
            _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData).Wait();
        }

    }
}