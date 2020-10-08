namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// authorization configuration.
    /// </summary>
    internal sealed class AuthorizationConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}Authorization";
    }
}
