// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Threading.Tasks;

namespace Tiny.Framework.Web.Redis.Services
{
    /// <summary>
    /// i provide cache storage (contract).
    /// </summary>
    public interface IProvideCacheStorage
    {
        /// <summary>
        /// get the item for...
        /// </summary>
        /// <param name="cacheKey">the cache key.</param>
        /// <returns>an instance of <typeparamref name="TCacheItem"/>.</returns>
        Task<TCacheItem> GetItemFor<TCacheItem>(string cacheKey);

        /// <summary>
        /// set the item for...
        /// </summary>
        /// <param name="cacheKey">the cache key.</param>
        /// <param name="cacheItem">the cache item.</param>
        /// <returns>the currently running task.</returns>
        Task SetItemFor<TCacheItem>(string cacheKey, TCacheItem cacheItem);

        /// <summary>
        /// clear the item for...
        /// </summary>
        /// <param name="cacheKey">the cache key.</param>
        /// <returns>the currently running task.</returns>
        Task ClearItemFor(string cacheKey);
    }
}
