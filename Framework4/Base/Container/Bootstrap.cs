//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Tiny.Framework.Container.Internal;
using Tiny.Framework.Diagnostic;
using Tiny.Framework.Diagnostic.Internal;
using Tiny.Framework.IO;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the bootstrapper.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// _for assemblies.
        /// </summary>
        private const string _forAssemblies = @"participating_assemblies.json";

        /// <summary>
        /// Configures a lifetime container.
        /// </summary>
        /// <param name="openResource">the open resource stream used on android.</param>
        public static void ConfigureLifetimeContainer(Func<string, Stream> openResource)
        {
            if (MEFContainer.IsConfigured())
            {
                return;
            }

            Asset.Manager.SetResourceCallback(openResource);
            var cache = LoadType<List<string>>(_forAssemblies);
            var assemblies = GetAssemblies(cache);

            var container = CreateContainer(assemblies);
            MEFContainer.SetContainer(container);
        }

        /// <summary>
        /// Loads the type.
        /// </summary>
        /// <typeparam name="T">the type to load.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>a rehydrated configuration type.</returns>
        public static T LoadType<T>(string fileName)
            where T : class
        {
            var fileContent = Asset.Manager.GetTextAsset(fileName).Result;
            return JsonConvert.DeserializeObject<T>(fileContent);
        }

        /// <summary>
        /// Resolves T from the container.
        /// </summary>
        /// <typeparam name="TShell">the type of the T to be resolved.</typeparam>
        /// <returns>an instance of <typeparamref name="TShell"/>.</returns>
        public static TShell ResolveShell<TShell>()
            where TShell : IShell
        {
            if (MEFContainer.IsConfigured())
            {
                // load the logging propagator
                _ = MEFContainer.Resolve<IPropagateLogging>();

                var shell = MEFContainer.Resolve<TShell>();
                ResolveImports(shell);

                return shell;
            }

            throw new InvalidOperationException("container not configured...");
        }

        /// <summary>
        /// resolves imports on those occasions we have to do it manually.
        /// </summary>
        /// <param name="item">the item on which the imports need to be resolved.</param>
        public static void ResolveImports(object item)
        {
            MEFContainer.ResolveImports(item);
        }

        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <param name="assemblies">the assemblies.</param>
        /// <returns>a composition host.</returns>
        private static CompositionHost CreateContainer(IReadOnlyCollection<Assembly> assemblies)
        {
            return new ContainerConfiguration()
                .WithAssemblies(assemblies)
                .CreateContainer();
        }

        /// <summary>
        /// gets the assemblies.
        /// </summary>
        /// <param name="participatingFiles">the participating files.</param>
        /// <returns>a readonly assembly list.</returns>
        private static IReadOnlyCollection<Assembly> GetAssemblies(IReadOnlyCollection<string> participatingFiles)
        {
            Logger.AddEntry("mef participating files:");
            participatingFiles
                .ForEach(x => Logger.AddEntry(x));

            return participatingFiles
                .Select(x => Assembly.Load(new AssemblyName(x)))
                .AsSafeReadOnlyList();
        }
    }
}
