using System;
using Microsoft.EntityFrameworkCore;
using Tiny.Framework.Data.Configuration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Data.Factories
{
    /// <summary>
    /// the data access scope abstract class.
    /// </summary>
    public abstract class DataAccessScope :
        DbContext,
        IScopeAccessToData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessScope"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected DataAccessScope(IConfigureContextScope configuration)
            : base(configuration?.GetContextOptions())
        {
            It.IsNull(configuration)
                .AsGuard<ArgumentNullException>(nameof(configuration));
        }
    }
}
