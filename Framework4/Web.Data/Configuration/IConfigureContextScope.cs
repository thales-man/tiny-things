using Microsoft.EntityFrameworkCore;

namespace Tiny.Framework.Data.Configuration
{
    /// <summary>
    /// I configure entitlement sync (contract).
    /// </summary>
    public interface IConfigureContextScope
    {
        /// <summary>
        /// gets the connection details.
        /// </summary>
        string ConnectionDetails { get; }

        /// <summary>
        /// get context options.
        /// </summary>
        /// <returns>The context options.</returns>
        DbContextOptions GetContextOptions();
    }
}