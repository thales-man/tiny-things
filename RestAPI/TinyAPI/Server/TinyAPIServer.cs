//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Composition;
using System.Net;
using System.Threading.Tasks;
using Http.Service.Contract.Abstracts;
using Http.Service.Contract.Boundaries;
using Tiny.API.Contracts;
using Tiny.Framework.Container;

namespace Tiny.API
{
    /// <summary>
    /// the tiny API server
    /// </summary>
    /// <seealso cref="HttpServerBase" />
    /// <seealso cref="IBootstrapServer" />
    [Export(typeof(IBootstrapServer))]
    public class TinyAPIServer : HttpServerBase, IBootstrapServer
    {
        /// <summary>
        /// Gets or sets the server request creator 
        /// </summary>
        [Import]
        public ICreateServerRequests ServerRequest { get; set; }

        /// <summary>
        /// Gets or sets the request handler
        /// </summary>
        [Import]
        public IHandleEndpointRequests Request { get; set; }

        /// <summary>
        /// Gets or sets the controler meta data provider
        /// </summary>
        [Import]
        public IProvideEndpointMethods MetaData { get; set; }

        /// <summary>
        /// Gets or sets the server response creator
        /// </summary>
        [Import]
        public ICreateServerResponses ServerResponse { get; set; }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The incoming.</param>
        /// <returns>the http response reader back to the base class</returns>
        public override async Task<IReadHttpResponse> HandleRequest(IReadHttpRequest request)
        {
            var serverRequest = ServerRequest.Create(request);
            var response = await Request.Handle(serverRequest);

            return ServerResponse.Create(response, serverRequest);
        }

        /// <summary>
        /// Starts the service on the specified port
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="servicePrefix">The service prefix.</param>
        /// <returns>a task</returns>
        public void Start(IPAddress address, ushort port, string servicePrefix)
        {
            MetaData.Compose(servicePrefix);
            StartServer(address, port);
        }
    }
}
