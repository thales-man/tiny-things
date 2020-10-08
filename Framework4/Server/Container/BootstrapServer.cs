//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Container
{
    /// <summary>
    /// the bootstrapper
    /// </summary>
    public static class BootstrapServer
    {
        /// <summary>
        /// for configuration
        /// </summary>
        private const string _forConfiguration = @"service_configuration.xml";

        /// <summary>
        /// Bootstrap the server.
        /// </summary>
        /// <returns>a bootstrap server</returns>
        public static IShell Start(IServiceConfiguration configCandidate = null)
        {
            var config = configCandidate ?? GetConfiguration();
            var server = Load();

            server.Start(config.GetListeningAddress(), config.ListeningPort, config.ServicePrefix);

            return server;
        }

        /// <summary>
        /// Load the server.
        /// </summary>
        /// <returns>a bootstrap server</returns>
        internal static IBootstrapServer Load()
        {
            Bootstrap.ConfigureLifetimeContainer(null);

            return Bootstrap.ResolveShell<IBootstrapServer>();
        }

        /// <summary>
        /// Get the service configuration
        /// </summary>
        /// <returns>a service configuration</returns>
        internal static IServiceConfiguration GetConfiguration()
        {
            return Bootstrap.LoadType<ServiceConfiguration>(_forConfiguration);
        }
    }
}
