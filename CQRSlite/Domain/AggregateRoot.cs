using CQRSlite.Domain.Exception;
using CQRSlite.Events;
using CQRSlite.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSlite.Domain
{
    /// <summary>
    /// Class to inherit all aggregates from.
    /// </summary>
    public abstract class AggregateRoot
    {
        public AggregateRoot(){}
        protected AggregateRoot(Identity identity){
            this.Identity=identity;
        }
        private readonly List<IEvent> _changes = new List<IEvent>();

        public Identity Identity { get; protected set; }
        public int Version { get; protected set; }

        public IEvent[] GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        /// <summary>
        /// Returns all uncommitted changes and clears aggregate of them.
        /// </summary>
        /// <returns>Array of new uncommitted events</returns>
        public IEvent[] FlushUncommittedChanges()
        {
            lock (_changes)
            {
                var changes = _changes.ToArray();
                var i = 0;
                foreach (var e in changes)
                {
                    if (e.Identity == default && Identity == default)
                    {
                        throw new AggregateOrEventMissingIdException(GetType(), e.GetType());
                    }
                    if (e.Identity == default)
                    {
                        e.Identity = Identity;
                    }
                    if (e.Identity != Identity)
                    {
                        throw new EventIdIncorrectException(e.Identity, Identity);
                    }
                    i++;
                    e.Version = Version + i;
                    e.TimeStamp = DateTimeOffset.UtcNow;
                }
                Version = Version + changes.Length;
                _changes.Clear();
                return changes;
            }
        }

        /// <summary>
        /// Load an aggregate from an enumerable of events.
        /// </summary>
        /// <param name="history">All events to be loaded.</param>
        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            lock (_changes)
            {
                foreach (var e in history.ToArray())
                {
                    if (e.Version != Version + 1)
                    {
                        throw new EventsOutOfOrderException(e.Identity);
                    }
                    if (e.Identity != Identity && Identity != default)
                    {
                        throw new EventIdIncorrectException(e.Identity, Identity);
                    } 
                    ApplyEvent(e);
                    Identity = e.Identity;
                    Version++;
                }
            }
        }

        protected void ApplyChange(IEvent @event)
        {
            lock (_changes)
            {
                ApplyEvent(@event);
                _changes.Add(@event);
            }
        }

        /// <summary>
        /// Overrideable method for applying events on aggregate
        /// This is called internally when rehydrating aggregates.
        /// Can be overridden if you want custom handling.
        /// </summary>
        /// <param name="event">Event to apply</param>
        protected virtual void ApplyEvent(IEvent @event)
        {
            this.Invoke("Apply", @event);
        }
    }
}
