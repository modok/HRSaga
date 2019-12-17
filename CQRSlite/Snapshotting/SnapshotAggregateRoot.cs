using CQRSlite.Domain;

namespace CQRSlite.Snapshotting
{
    /// <inheritdoc />
    /// <summary>
    /// Class to inherit aggregates that should be snapshotted from.
    /// </summary>
    /// <typeparam name="T">Type of memento object capturing aggregate state</typeparam>
    public abstract class SnapshotAggregateRoot<T> : AggregateRoot where T : Snapshot
    {
        public T GetSnapshot()
        {
            var snapshot = CreateSnapshot();
            snapshot.Identity = Identity;
            return snapshot;
        }

        public void Restore(T snapshot)
        {
            Identity = snapshot.Identity;
            Version = snapshot.Version;
            RestoreFromSnapshot(snapshot);
        }

        protected abstract T CreateSnapshot();
        protected abstract void RestoreFromSnapshot(T snapshot);
    }

}
