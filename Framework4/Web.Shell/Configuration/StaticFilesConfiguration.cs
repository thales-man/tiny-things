namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// static files configuration.
    /// </summary>
    internal sealed class StaticFilesConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}StaticFiles";
    }
}
