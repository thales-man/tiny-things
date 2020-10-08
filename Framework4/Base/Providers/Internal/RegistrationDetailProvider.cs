using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tiny.Framework.IO;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Providers.Internal
{
    /// <summary>
    /// the generic host registration details provider (implementation).
    /// </summary>
    internal sealed class RegistrationDetailProvider :
        IProvideRegistrationDetails,
        ISupportServiceRegistration
    {
        private const string _forAssemblies = @"participating_assemblies.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationDetailProvider"/> class.
        /// </summary>
        /// <param name="assets">the assets providers.</param>
        /// <param name="converter">the json converter.</param>
        public RegistrationDetailProvider(IManageAssets assets, ISerializeJsonTypes converter)
        {
            It.IsNull(assets)
                .AsGuard<ArgumentNullException>(nameof(assets));
            It.IsNull(converter)
                .AsGuard<ArgumentNullException>(nameof(converter));

            Assets = assets;
            Converter = converter;
        }

        /// <summary>
        /// gets the asset provider.
        /// </summary>
        internal IManageAssets Assets { get; }

        /// <summary>
        /// gets the json convewrter.
        /// </summary>
        internal ISerializeJsonTypes Converter { get; }

        /// <summary>
        /// Load.
        /// </summary>
        /// <returns>A reaodnly collection of assembly.</returns>
        public async Task<IReadOnlyCollection<Assembly>> Load()
        {
            var content = await Assets.GetTextAsset(_forAssemblies);
            var list = Converter.FromString<List<string>>(content);
            return GetAssemblies(list);
        }

        /// <summary>
        /// Get the attribute list for...
        /// </summary>
        /// <typeparam name="TAttribute">the type of attribute.</typeparam>
        /// <param name="registrant">(the) registrant.</param>
        /// <returns>the attribute list.</returns>
        public IReadOnlyCollection<TAttribute> GetAttributeListFor<TAttribute>(Assembly registrant)
            where TAttribute : Attribute
        {
            It.IsNull(registrant)
                .AsGuard<ArgumentNullException>(nameof(registrant));

            return registrant
                .GetCustomAttributes<TAttribute>()
                .AsSafeReadOnlyList();
        }

        /// <summary>
        /// gets the assemblies.
        /// </summary>
        /// <param name="participatingFiles">the participating files.</param>
        /// <returns>A readonly assembly list.</returns>
        internal IReadOnlyCollection<Assembly> GetAssemblies(IReadOnlyCollection<string> participatingFiles) =>
            participatingFiles
                .Select(x => Assembly.Load(new AssemblyName(x)))
            .AsSafeReadOnlyList();
    }
}