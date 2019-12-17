using System;
using CQRSlite.Domain;

namespace CQRSlite.Snapshotting
{
    /// <summary>
    /// A memento object of a aggregate in a version.
    /// </summary>
    public abstract class Snapshot
    {
        public Identity Identity { get; set; }
        public int Version { get; set; }
    }
}
