namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// slugify transformer configuration.
    /// </summary>
    internal sealed class SlugifyTransformerConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}SlugifyTransformer";
    }
}
