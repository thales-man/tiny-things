// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tiny.Framework.Web.Shell
{
    /// <summary>
    /// i shell startup.
    /// </summary>
    public interface IShellStartup
    {
        /// <summary>
        /// use services.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        void Configure(IApplicationBuilder builder, IHostEnvironment env);

        /// <summary>
        /// add services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        void ConfigureServices(IServiceCollection services);
    }
}