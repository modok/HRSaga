using System;
using System.Threading.Tasks;
using CQRSlite.Domain;

namespace CQRSlite.Caching
{
    /// <summary>
    /// Defines a cache implementation
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Check if aggregate is tracked.
        /// </summary>
        /// <param name="identity">Id of aggregate</param>
        /// <returns>Task representing operation. Task result if is tracked</returns>
        Task<bool> IsTracked(Identity identity);

        /// <summary>
        /// Adds Aggregate to cache
        /// </summary>
        /// <param name="identity">Id of aggregate</param>
        /// <param name="aggregate">The aggregate to cache</param>
        /// <returns>Task representing operation</returns>
        Task Set(Identity identity, AggregateRoot aggregate);

        /// <summary>
        /// Get aggregate from cache
        /// </summary>
        /// <param name="identity">Id of aggregate</param>
        /// <returns>Task representing operation. Task result is aggregate</returns>
        Task<AggregateRoot> Get(Identity identity);

        /// <summary>
        /// Remove aggregate from cache
        /// </summary>
        /// <param name="identity">Id of aggregate</param>
        /// <returns>Task representing operation</returns>
        Task Remove(Identity identity);

        /// <summary>
        /// Register a callback action to be called when a cached object is evicted from cache.
        /// </summary>
        /// <param name="action">Action to be called</param>
        void RegisterEvictionCallback(Action<Identity> action);
    }
}
