using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Tiny.Framework.Providers
{
    /// <summary>
    /// I provide registration services (contract).
    /// </summary>
    public interface IProvideRegistrationServices
    {
        /// <summary>
        /// Register with...
        /// </summary>
        /// <param name="containerCollection">the container collection.</param>
        void RegisterWith(IServiceCollection containerCollection);

        /// <summary>
        /// build with...
        /// </summary>
        /// <param name="builder">the application builder.</param>
        void BuildWith(IApplicationBuilder builder);
    }
}