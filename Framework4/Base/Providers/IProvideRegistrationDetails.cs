using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Tiny.Framework.Providers
{
    /// <summary>
    /// I provide registration details (contract).
    /// </summary>
    public interface IProvideRegistrationDetails
    {
        /// <summary>
        /// Load.
        /// </summary>
        /// <returns>A reaodnly collection of assembly.</returns>
        Task<IReadOnlyCollection<Assembly>> Load();

        /// <summary>
        /// Get the attribute list for...
        /// </summary>
        /// <typeparam name="TAttribute">the type of attribute.</typeparam>
        /// <param name="registrant">(the) registrant.</param>
        /// <returns>the attribute list.</returns>
        IReadOnlyCollection<TAttribute> GetAttributeListFor<TAttribute>(Assembly registrant)
            where TAttribute : Attribute;
    }
}