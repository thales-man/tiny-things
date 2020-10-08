//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Net;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// i bootstrap servers
    /// </summary>
    public interface IBootstrapServer :
        IShell
    {
        /// <summary>
        /// Starts the service on the specified address and port with a prefix.
        /// http://my.service.com[:80/api]...
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="servicePrefix">The service prefix.</param>
        /// <returns>a task</returns>
        void Start(IPAddress address, ushort port = 80, string servicePrefix = null);
    }
}
