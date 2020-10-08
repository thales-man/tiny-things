//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.IO;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the bootstrapper
    /// </summary>
    public static class BootstrapUI
    {
        /// <summary>
        /// start the application.
        /// 
        /// the shell is sent into the callback because we don't know how 
        /// much work needs to be done once the shell is resolved
        /// </summary>
        /// <typeparam name="TShell">The type of shell.</typeparam>
        /// <param name="callbackTo">The shell loading action</param>
        public static void Start<TShell>(Action<TShell> callbackTo, Func<string, Stream> openResource = null)
            where TShell : class, IShell
        {
            Bootstrap.ConfigureLifetimeContainer(openResource);

            var shell = Bootstrap.ResolveShell<TShell>();

            callbackTo(shell);
        }
    }
}
