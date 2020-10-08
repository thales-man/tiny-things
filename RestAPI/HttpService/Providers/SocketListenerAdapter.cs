//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Composition;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;

namespace Http.Service.Provider
{
    /// <summary>
    /// Socket listener adapter.
    /// </summary>
    [Shared]
    [Export(typeof(IListeningSocketAdapter))]
    internal sealed class SocketListenerAdapter :
        IListeningSocketAdapter
    {
        /// <summary>
        /// is listening.
        /// </summary>
        public bool IsListening { get; set; }

        /// <summary>
        /// Gets or sets the local end point.
        /// </summary>
        /// <value>The local end point.</value>
        public IPEndPoint LocalEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        [Import]
        public ExportFactory<IAdaptSocketStreams> Factory { get; set; }

        /// <summary>
        /// Start using the specified localEndpoint and requestProcessor.
        /// </summary>
        /// <param name="localEndpoint">Local endpoint.</param>
        /// <param name="requestProcessor">Request processor.</param>
        public void Start(IPEndPoint localEndpoint, Func<IAdaptSocketStreams, Task> requestProcessor)
        {
            //TODO: critical section?
            IsListening = true;
            LocalEndPoint = localEndpoint;

            var _tcpListener = new TcpListener(localEndpoint);
            _tcpListener.Start();

            Task.Run(async () =>
            {
                //TODO: critical section?
                while (IsListening)
                {
                    var client = await _tcpListener.AcceptTcpClientAsync();

                    using (var export = Factory.CreateExport())
                    {
                        var clientSocket = export.Value;
                        clientSocket.Compose(client);
                        await requestProcessor(clientSocket);
                    }
                }

                _tcpListener.Stop();
            });
        }

        //TODO: critical section?
        public void Stop() => IsListening = false;
    }
}