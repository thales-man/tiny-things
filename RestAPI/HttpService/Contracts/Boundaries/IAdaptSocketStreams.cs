//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Net.Sockets;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// I adapt socket streams.
    /// </summary>
    public interface IAdaptSocketStreams :
        IStreamIn,
        IStreamOut,
        IDisposable
    {
        void Compose(TcpClient tcpClient);
    }
}