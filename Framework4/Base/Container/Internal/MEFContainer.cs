//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Composition.Hosting;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Container.Internal
{
    /// <summary>
    /// the composition container
    /// this acts a both an injectable and static class (bootstrapping process).
    /// </summary>
    /// <seealso cref="IResolveTypes" />
    [Shared]
    [Export(typeof(IResolveTypes))]
    internal sealed class MEFContainer :
            IResolveTypes
    {
        /// <summary>
        /// gets or sets the container.
        /// </summary>
        private static CompositionHost Container { get; set; }

        /// <summary>
        /// Determines whether this instance is configured.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is configured; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsConfigured()
        {
            return It.Has(Container);
        }

        /// <summary>
        /// Sets the container.
        /// </summary>
        /// <param name="container">the container.</param>
        public static void SetContainer(CompositionHost container)
        {
            Container = container;
        }

        /// <summary>
        /// Resolves T from the container.
        /// </summary>
        /// <typeparam name="T">the type of the T to be resolved.</typeparam>
        /// <returns>an instance of T.</returns>
        public static T Resolve<T>()
        {
            It.IsNull(Container)
                .AsGuard<ArgumentNullException>();

            return Container.GetExport<T>();
        }

        /// <summary>
        /// Resolves the imports.
        /// </summary>
        /// <param name="target">the target.</param>
        public static void ResolveImports(object target)
        {
            It.IsNull(Container)
                .AsGuard<ArgumentNullException>();

            Container.SatisfyImports(target);
        }

        /// <summary>
        /// Resolves T from the container.
        /// </summary>
        /// <typeparam name="T">the type of the T to be resolved.</typeparam>
        /// <returns>an instance of T.</returns>
        T IResolveTypes.Resolve<T>()
        {
            return Resolve<T>();
        }

        /// <summary>
        /// Resolves the imports.
        /// </summary>
        /// <param name="target">the target.</param>
        void IResolveTypes.ResolveImports(object target)
        {
            ResolveImports(target);
        }

        /// <summary>
        /// gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>
        /// A service object of type <paramref name="serviceType">serviceType</paramref>.   -or-  null if there is no service object of type <paramref name="serviceType">serviceType</paramref>.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return Container.GetExport(serviceType);
        }
    }
}