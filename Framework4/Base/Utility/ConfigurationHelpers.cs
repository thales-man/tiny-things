using Microsoft.Extensions.Configuration;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// Extension methods for IConfiguration.
    /// </summary>
    public static class ConfigurationHelpers
    {
        /// <summary>
        ///  Attempts to load the configuration section into a new object of class T.
        /// </summary>
        /// <typeparam name="T">the configuration class type.</typeparam>
        /// <param name="configuration">the application configuration.</param>
        /// <param name="sectionName">the section name.</param>
        /// <returns>the configuration object of class T.</returns>
        public static T LoadSection<T>(this IConfiguration configuration, string sectionName)
            where T : new()
        {
            var result = new T();
            configuration.Bind(sectionName, result);

            return result;
        }
    }
}