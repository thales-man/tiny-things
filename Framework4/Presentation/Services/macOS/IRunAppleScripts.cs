using System.Threading.Tasks;

namespace Tiny.Framework.Services.macOS
{
    /// <summary>
    /// I run apple scripts.
    /// </summary>
    public interface IRunAppleScripts
    {
        /// <summary>
        /// Run the specified Script.
        /// </summary>
        /// <returns>The run response.</returns>
        /// <param name="theScript">The script.</param>
        Task<string> Run(string theScript);
    }
}
