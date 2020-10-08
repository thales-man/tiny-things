using System;
using Tiny.Framework.Data.Configuration;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Data.Factories
{
    /// <summary>
    /// the generic data access scope factory.
    /// </summary>
    /// <typeparam name="TContextScope">the context scope.</typeparam>
    /// <typeparam name="TContractScope">the context scope contract.</typeparam>
    public abstract class ContextScopeFactory<TContextScope, TContractScope> :
        ICreateAccessScopes<TContractScope>,
        ISupportServiceRegistration
        where TContextScope : DataAccessScope, TContractScope
        where TContractScope : class, IScopeAccessToData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextScopeFactory{TContextScope, TContractScope}"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected ContextScopeFactory(IConfigureContextScope configuration)
        {
            It.IsNull(configuration)
                .AsGuard<ArgumentNullException>(nameof(configuration));

            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        internal IConfigureContextScope Configuration { get; }

        /// <inheritdoc/>
        public TContractScope BeginScope() =>
            CreateScope(Configuration);

        /// <summary>
        /// create scope.
        /// </summary>
        /// <param name="configureContext">the context configuration.</param>
        /// <returns>an new instance of a context scope.</returns>
        public abstract TContextScope CreateScope(IConfigureContextScope configureContext);
    }
}