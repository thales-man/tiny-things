using Microsoft.EntityFrameworkCore;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Data.Configuration
{
    /// <summary>
    /// context scope configuration.
    /// </summary>
    /// <typeparam name="TContextScope">the db context scope.</typeparam>
    public abstract class ContextScopeConfiguration<TContextScope> :
        IConfigureContextScope,
        ISupportConfigurationRegistration
        where TContextScope : DbContext
    {
        /// <inheritdoc/>
        public string ConnectionDetails { get; set; }

        /// <inheritdoc/>
        public DbContextOptions GetContextOptions()
        {
            var builder = new DbContextOptionsBuilder<TContextScope>();
            LoadProvider(builder);

            // builder.UseSqlServer(ConnectionDetails)
            // builder.UseSqlite(ConnectionDetails)
            return builder.Options;
        }

        /// <summary>
        /// load the provider.
        /// </summary>
        /// <param name="builder">the builder.</param>
        public abstract void LoadProvider(DbContextOptionsBuilder<TContextScope> builder);
    }
}