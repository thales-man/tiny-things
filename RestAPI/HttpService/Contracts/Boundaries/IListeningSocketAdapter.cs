//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// I listening socket adapter.
    /// </summary>
    public interface IListeningSocketAdapter
    {
        /// <summary>
        /// Gets a value indicating whether this
        /// <see cref="T:Http.Service.Contract.Boundaries.IListeningSocketAdapter"/> is listening.
        /// </summary>
        /// <value><c>true</c> if is listening; otherwise, <c>false</c>.</value>
        bool IsListening { get; }

        /// <summary>
        /// Gets the local end point.
        /// </summary>
        IPEndPoint LocalEndPoint { get; }

        /// <summary>
        /// Start using the specified localEndpoint and requestProcessor.
        /// </summary>
        /// <param name="localEndpoint">Local endpoint.</param>
        /// <param name="requestProcessor">Request processor.</param>
        void Start(IPEndPoint localEndpoint, Func<IAdaptSocketStreams, Task> requestProcessor);

        /// <summary>
        /// Stop this instance.
        /// </summary>
        void Stop();
    }
}