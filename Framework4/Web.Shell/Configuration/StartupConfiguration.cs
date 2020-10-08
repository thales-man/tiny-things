namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// startup configuration.
    /// </summary>
    internal abstract class StartupConfiguration
    {
        /// <summary>
        /// gets the group settings key.
        /// </summary>
        public static string GroupSettingsKey => "Startup:";

        /// <summary>
        /// gets or sets a value indicating whether the activated value.
        /// </summary>
        public bool IsActivated { get; set; } = false;
    }
}
