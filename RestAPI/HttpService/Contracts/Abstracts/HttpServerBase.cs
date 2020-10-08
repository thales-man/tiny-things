//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Net;
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Providers;
using Tiny.Framework.Diagnostic;
using Tiny.Framework.Flow;
using Tiny.Framework.Utility;

namespace Http.Service.Contract.Abstracts
{
    /// <summary>
    /// the http server base abstract class
    /// </summary>
    public abstract class HttpServerBase
    {
        /// <summary>
        /// Gets or sets the socket listener.
        /// </summary>
        /// <value>The socket listener.</value>
        [Import]
        public IListeningSocketAdapter SocketListener { get; set; }

        /// <summary>
        /// Gets or sets the input stream parser
        /// </summary>
        [Import]
        public IParseHttpRequest InputStream { get; set; }

        /// <summary>
        /// Gets or sets the output stream converter
        /// </summary>
        [Import]
        public IParseHttpResponse Convert { get; set; }

        /// <summary>
        /// Gets or sets the mediator.
        /// </summary>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic message factory
        /// </summary>
        [Import]
        public ICreateDiagnosticMessages Message { get; set; }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>the http read response</returns>
        public abstract Task<IReadHttpResponse> HandleRequest(IReadHttpRequest request);

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="serverPort">The server port.</param>
        /// <returns>a task</returns>
        public void StartServer(IPAddress address, ushort serverPort)
        {
            StopServer();
            SocketListener.Start(new IPEndPoint(address, serverPort), ProcessRequest);
            Mediator.Publish(Message.Create($"server started on '{SocketListener.LocalEndPoint}'"));
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void StopServer()
        {
            if (SocketListener.IsListening)
            {
                SocketListener.Stop();
                Mediator.Publish(Message.Create($"server on '{SocketListener.LocalEndPoint}' stopped"));
            }
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="clientSocket">Client socket.</param>
        public async Task ProcessRequest(IAdaptSocketStreams clientSocket)
        {
            It.IsNull(clientSocket)
                .AsGuard<ArgumentNullException>(nameof(clientSocket));

            try
            {
                var request = await GetRequest(clientSocket);
                var response = await HandleRequest(request);
                await WriteResponse(clientSocket, response);
            }
            catch (Exception ex)
            {
                Mediator.Publish(Message.Create($"an exception has occurred '{ex.Message}'"));
            }
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <returns>a http request</returns>
        public async Task<IHttpRequest> GetRequest(IStreamIn socket)
        {
            return await InputStream.Parse(socket);
        }

        /// <summary>
        /// Writes the response.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="response">The response.</param>
        /// <returns>a task</returns>
        public async Task WriteResponse(IStreamOut socket, IReadHttpResponse response)
        {
            var buffer = Convert.ToBuffer(response);
            await socket.Write(buffer);
            await socket.Flush();
        }
    }
}
