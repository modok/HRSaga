using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.ReadModels;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Queries
{
    public class GetAllCaptainQuery : IQuery<IReadOnlyCollection<CaptainReadModel>>
    {
    }
    public class GetAllCaptainsQueryHandler : IQueryHandler<GetAllCaptainQuery, IReadOnlyCollection<CaptainReadModel>>
    {
        private readonly IInMemoryReadStore<CaptainReadModel> _readStore;

        public GetAllCaptainsQueryHandler(IInMemoryReadStore<CaptainReadModel> readStore){
            _readStore = readStore;
        }
        public async Task<IReadOnlyCollection<CaptainReadModel>> ExecuteQueryAsync(GetAllCaptainQuery query, CancellationToken cancellationToken)
        {
            var captainReadModels = await _readStore.FindAsync(rm => true, cancellationToken).ConfigureAwait(false);
            return captainReadModels.ToList();
        }
    }
}
