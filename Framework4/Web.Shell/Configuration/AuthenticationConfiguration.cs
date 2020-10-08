namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// authentication configuration.
    /// </summary>
    internal sealed class AuthenticationConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}Authentication";
    }
}
