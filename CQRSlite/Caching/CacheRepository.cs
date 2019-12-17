using CQRSlite.Domain;
using CQRSlite.Events;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSlite.Caching
{
    /// <summary>
    /// Thread safe repository decorator that can cache aggregates.
    /// </summary>
    public class CacheRepository : IRepository
    {
        private readonly IRepository _repository;
        private readonly IEventStore _eventStore;
        private readonly ICache _cache;

        private static readonly ConcurrentDictionary<Identity, SemaphoreSlim> _locks =
            new ConcurrentDictionary<Identity, SemaphoreSlim>();

        private static SemaphoreSlim CreateLock(Identity _) => new SemaphoreSlim(1, 1);

        /// <summary>
        /// Initialize a new instance of CacheRepository
        /// </summary>
        /// <param name="repository">Reposiory that gets aggregate from event store</param>
        /// <param name="eventStore">Eventstore where concurrency checking can be fetched from</param>
        /// <param name="cache">Implementation of the cache</param>
        public CacheRepository(IRepository repository, IEventStore eventStore, ICache cache)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _cache.RegisterEvictionCallback(key => _locks.TryRemove(key, out var _));
        }

        public async Task Save<T>(T aggregate, int? expectedVersion = null,
            CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            var @lock = _locks.GetOrAdd(aggregate.Identity, CreateLock);
            await @lock.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (aggregate.Identity != default && !await _cache.IsTracked(aggregate.Identity).ConfigureAwait(false))
                {
                    await _cache.Set(aggregate.Identity, aggregate).ConfigureAwait(false);
                }
                await _repository.Save(aggregate, expectedVersion, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await _cache.Remove(aggregate.Identity).ConfigureAwait(false);
                throw;
            }
            finally
            {
                @lock.Release();
            }
        }

        public async Task<T> Get<T>(Identity aggregateIdentity, CancellationToken cancellationToken = default)
            where T : AggregateRoot
        {
            var @lock = _locks.GetOrAdd(aggregateIdentity, CreateLock);
            await @lock.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                T aggregate;
                if (await _cache.IsTracked(aggregateIdentity).ConfigureAwait(false))
                {
                    aggregate = (T) await _cache.Get(aggregateIdentity).ConfigureAwait(false);
                    var events = await _eventStore.Get(aggregateIdentity, aggregate.Version, cancellationToken).ConfigureAwait(false);
                    if (events.Any() && events.First().Version != aggregate.Version + 1)
                    {
                        await _cache.Remove(aggregateIdentity).ConfigureAwait(false);
                    }
                    else
                    {
                        aggregate.LoadFromHistory(events);
                        return aggregate;
                    }
                }

                aggregate = await _repository.Get<T>(aggregateIdentity, cancellationToken).ConfigureAwait(false);
                await _cache.Set(aggregateIdentity, aggregate).ConfigureAwait(false);
                return aggregate;
            }
            catch (Exception)
            {
                await _cache.Remove(aggregateIdentity).ConfigureAwait(false);
                throw;
            }
            finally
            {
                @lock.Release();
            }
        }
    }
}