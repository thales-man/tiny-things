// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.Redis.Configuration;

namespace Tiny.Framework.Web.Redis
{
    /// <summary>
    /// the redis startup registration routine.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "delegate mechanism")]
    public static class CacheRegistration
    {
        /// <summary>
        /// the add cacheing routine name.
        /// </summary>
        public const string AddCacheingRoutine = nameof(AddCacheing);

        /// <summary>
        /// Configures the services for the container.
        /// </summary>
        /// <param name="services">the service collection.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddCacheing(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<CacheStorageConfiguration>(CacheStorageConfiguration.AppSettingsKey);

            ColorConsole.Startup($"adding distributed cacheing with connection string: '{service.ConnectionString}'");

            services
                .AddDistributedRedisCache(options => options.Configuration = service.ConnectionString);
        }
    }
}