namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// https redirection configuration.
    /// </summary>
    internal sealed class HttpsRedirectionConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}HttpsRedirection";
    }
}
