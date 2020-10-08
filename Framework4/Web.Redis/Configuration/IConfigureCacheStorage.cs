// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Microsoft.Extensions.Caching.Distributed;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Web.Redis.Configuration
{
    /// <summary>
    /// I configure distributed cache options (contract).
    /// </summary>
    public interface IConfigureCacheStorage :
        ISupportConfigurationRegistration
    {
        /// <summary>
        /// Get the (distributed cache entry) options.
        /// </summary>
        /// <returns>the distributed cache entry options.</returns>
        DistributedCacheEntryOptions GetOptions();
    }
}
