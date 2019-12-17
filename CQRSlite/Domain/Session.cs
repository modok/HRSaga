using CQRSlite.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSlite.Domain
{
    /// <summary>
    /// Implementation of unit of work for aggregates.
    /// </summary>
    public class Session : ISession
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Identity, AggregateDescriptor> _trackedAggregates;

        /// <summary>
        /// Initialize Session
        /// </summary>
        /// <param name="repository"></param>
        public Session(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _trackedAggregates = new Dictionary<Identity, AggregateDescriptor>();
        }

        public Task Add<T>(T aggregate, CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Identity))
            {
                _trackedAggregates.Add(aggregate.Identity, new AggregateDescriptor { Aggregate = aggregate, Version = aggregate.Version });
            }
            else if (_trackedAggregates[aggregate.Identity].Aggregate != aggregate)
            {
                throw new ConcurrencyException(aggregate.Identity);
            }
            return Task.FromResult(0);
        }

        public async Task<T> Get<T>(Identity identity, int? expectedVersion = null, CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            if (IsTracked(identity))
            {
                var trackedAggregate = (T)_trackedAggregates[identity].Aggregate;
                if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
                {
                    throw new ConcurrencyException(trackedAggregate.Identity);
                }
                return trackedAggregate;
            }

            var aggregate = await _repository.Get<T>(identity, cancellationToken).ConfigureAwait(false);
            if (expectedVersion != null && aggregate.Version != expectedVersion)
            {
                throw new ConcurrencyException(identity);
            }
            await Add(aggregate, cancellationToken).ConfigureAwait(false);

            return aggregate;
        }

        private bool IsTracked(Identity identity)
        {
            return _trackedAggregates.ContainsKey(identity);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            try
            {
                var tasks = new Task[_trackedAggregates.Count];
                var i = 0;
                foreach (var descriptor in _trackedAggregates.Values)
                {
                    tasks[i] = _repository.Save(descriptor.Aggregate, descriptor.Version, cancellationToken);
                    i++;
                }
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            finally
            {
                _trackedAggregates.Clear();
            }
        }

        private class AggregateDescriptor
        {
            public AggregateRoot Aggregate { get; set; }
            public int Version { get; set; }
        }
    }
}
