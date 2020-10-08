// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using Microsoft.Extensions.Caching.Distributed;

namespace Tiny.Framework.Web.Redis.Configuration
{
    /// <summary>
    /// cache storage configuration (implenentation).
    /// </summary>
    internal sealed class CacheStorageConfiguration :
        IConfigureCacheStorage
    {
        /// <summary>
        /// the app settings key.
        /// </summary>
        public const string AppSettingsKey = "StorageCache";

        /// <summary>
        /// gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// gets or sets item lifetime in minutes.
        /// </summary>
        public int ItemLifetimeInMinutes { get; set; } = 60;

        // the distributed cache options.
        private DistributedCacheEntryOptions _options;

        /// <inheritdoc/>
        public DistributedCacheEntryOptions GetOptions() =>
            _options ??= new DistributedCacheEntryOptions
            {
                SlidingExpiration = new TimeSpan(0, ItemLifetimeInMinutes, 0)
            };
    }
}
