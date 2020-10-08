//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the dependency injection container.
    /// </summary>
    public interface IResolveTypes :
        IServiceProvider
    {
        /// <summary>
        /// Resolves T from the container.
        /// </summary>
        /// <typeparam name="T">the type to be resolved.</typeparam>
        /// <returns>An instance of T.</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves the imports.
        /// </summary>
        /// <param name="target">the target.</param>
        void ResolveImports(object target);
    }
}
