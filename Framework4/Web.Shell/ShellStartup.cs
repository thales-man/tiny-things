// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Providers;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Shell
{
    /// <summary>
    /// the startup class.
    /// </summary>
    public abstract class ShellStartup : IShellStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellStartup"/> class.
        /// </summary>
        /// <param name="configuration">the application configuration.</param>
        /// <param name="environment">the hosting environment the application is running in.</param>
        protected ShellStartup(IConfiguration configuration, IHostEnvironment environment)
        {
            It.IsNull(configuration)
                .AsGuard<ArgumentNullException>(nameof(configuration));
            It.IsNull(environment)
                .AsGuard<ArgumentNullException>(nameof(environment));

            Registrar = GenericHostRegistrar.Create(configuration, environment);
        }

        /// <summary>
        /// gets the registrar.
        /// </summary>
        internal IProvideRegistrationServices Registrar { get; }

        /// <inheritdoc/>
        public void ConfigureServices(IServiceCollection services)
        {
            Registrar.RegisterWith(services);
        }

        /// <inheritdoc/>
        public void Configure(IApplicationBuilder builder, IHostEnvironment env)
        {
            Registrar.BuildWith(builder);
        }
    }
}