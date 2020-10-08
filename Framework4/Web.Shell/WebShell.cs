// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Container;

namespace Tiny.Framework.Web.Shell
{
    /// <summary>
    /// the program.
    /// </summary>
    public static class WebShell
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <typeparam name="TStartup">the startup class.</typeparam>
        /// <param name="args">the command line arguments.</param>
        /// <returns>the host builder.</returns>
        public static IHost BuildWebHost<TStartup>(string[] args)
            where TStartup : class, IShellStartup
        {
            var host = Bootstrap.LoadType<HostConfiguration>(HostConfiguration.CommonFileName);

            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration(configure =>
                            configure.AddJsonFile("appsettings.json", false))
                        .UseContentRoot(host.ContentRootPath)
                        .UseWebRoot(host.WebRootFolderName)
                        .UseUrls(host.ServerListeningAddress)
                        .ConfigureServices(services =>
                            services.Add(new ServiceDescriptor(typeof(IHostConfiguration), host)))
                        .UseStartup<TStartup>();
                })
                .UseSystemd()
                .Build();
        }
    }
}