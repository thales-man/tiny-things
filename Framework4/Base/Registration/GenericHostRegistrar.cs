using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.IO.Internal;
using Tiny.Framework.Providers;
using Tiny.Framework.Providers.Internal;

namespace Tiny.Framework.Registration
{
    /// <summary>
    /// the service registration factory.
    /// </summary>
    public static class GenericHostRegistrar
    {
        /// <summary>
        /// Create...
        /// Because this is a bootrapping process,
        /// this class has to be static and we have to new things up.
        /// </summary>
        /// <param name="configuration">the configuration binder.</param>
        /// <param name="environment">the environment.</param>
        /// <returns>A service registrar.</returns>
        public static IProvideRegistrationServices Create(IConfiguration configuration, IHostEnvironment environment)
        {
            var assets = new AssetManager();
            var converter = new JsonConverter();
            var details = new RegistrationDetailProvider(assets, converter);

            return new ServiceRegistrationProvider(details, configuration, environment);
        }
    }
}
