using System;
using EventStore.ClientAPI;

namespace HRSaga.Adventure.Common.EventStore.Events
{
    public interface IEVEvent
    {
        String AggregateName { get; set; }
        Guid EventId { get; set; }

        EventData toEventData(bool json);

    }
}