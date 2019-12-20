using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace HRSaga.Adventure.Common.EventStore.Clients
{
    public interface IEVClient
    {
        IEventStoreConnection _connection {get;}
        //void ReadEvent(string ccceventstream);
        void AddEvent(EventData eventData, string streamName);
        Task<List<ResolvedEvent>> ReadEvent(string streamName);
    
        
    }
}