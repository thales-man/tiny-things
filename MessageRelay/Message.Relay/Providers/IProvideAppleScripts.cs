using MessageRelay.Models;

namespace MessageRelay.Providers
{
    /// <summary>
    /// I provide apple scripts.
    /// </summary>
    public interface IProvideAppleScripts
    {
        /// <summary>
        /// Gets the script for.
        /// </summary>
        /// <returns>The script for.</returns>
        /// <param name="theCommand">The command.</param>
        string GetScriptFor(MessageCommand theCommand);

        /// <summary>
        /// Gets the script for.
        /// </summary>
        /// <returns>The script for.</returns>
        /// <param name="theTrigger">The trigger.</param>
        string GetScriptFor(GeoFenceTrigger theTrigger);
    }
}
