//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Net;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the service configuration contact
    /// </summary>
    public interface IServiceConfiguration
    {
        /// <summary>
        /// gets the listening address
        /// </summary>
        /// <returns></returns>
        IPAddress GetListeningAddress();

        /// <summary>
        /// gets the listening port
        /// </summary>
        ushort ListeningPort { get; }

        /// <summary>
        /// gets the service prefix
        /// </summary>
        string ServicePrefix { get; }
    }
}