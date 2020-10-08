// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.Faults;
using Tiny.Framework.Web.Redis.Configuration;

namespace Tiny.Framework.Web.Redis.Services
{
    /// <summary>
    /// the cache storage provider (implementation).
    /// </summary>
    internal sealed class CacheStorageProvider :
        IProvideCacheStorage,
        ISupportServiceRegistration
    {
        /// <summary>
        /// gets the distributed cache.
        /// </summary>
        internal IDistributedCache Cache { get; }

        /// <summary>
        /// gets or sets the cache configuration options.
        /// </summary>
        internal IConfigureCacheStorage Config { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStorageProvider"/> class.
        /// </summary>
        /// <param name="cache">the distributed cache.</param>
        /// <param name="config">the distributed cache configuration options.</param>
        public CacheStorageProvider(
            IDistributedCache cache,
            IConfigureCacheStorage config)
        {
            It.IsNull(cache)
                .AsGuard<ArgumentNullException>(nameof(cache));
            It.IsNull(config)
                .AsGuard<ArgumentNullException>(nameof(config));

            Cache = cache;
            Config = config;
        }

        /// <inheritdoc/>
        public async Task<TCacheItem> GetItemFor<TCacheItem>(string cacheKey)
        {
            It.IsEmpty(cacheKey)
                .AsGuard<MalformedRequestException>(nameof(cacheKey));

            var result = await Cache.GetAsync(cacheKey);

            It.IsNull(result)
                .AsGuard<NoContentException>(nameof(cacheKey));


            var candidate = Encoding.UTF8.GetString(result);

            return JsonConvert.DeserializeObject<TCacheItem>(candidate);
        }

        /// <inheritdoc/>
        public async Task SetItemFor<TCacheItem>(string cacheKey, TCacheItem cacheItem)
        {
            It.IsEmpty(cacheKey)
                .AsGuard<MalformedRequestException>(nameof(cacheKey));

            var candidate = JsonConvert.SerializeObject(cacheItem);
            var bytes = Encoding.UTF8.GetBytes(candidate);

            await Cache.SetAsync(cacheKey, bytes, Config.GetOptions());
        }

        /// <inheritdoc/>
        public async Task ClearItemFor(string cacheKey)
        {
            It.IsEmpty(cacheKey)
                .AsGuard<MalformedRequestException>(nameof(cacheKey));

            await Cache.RemoveAsync(cacheKey);
        }
    }
}