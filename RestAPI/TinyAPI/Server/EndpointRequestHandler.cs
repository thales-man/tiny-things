//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Tiny.Framework.Utility;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using System.Composition;
using System.Threading.Tasks;
using Tiny.API.Contracts;
using System;

namespace Tiny.API
{
    /// <summary>
    /// the endpoint request handler
    /// </summary>
    /// <seealso cref="IHandleEndpointRequests" />
    [Shared]
    [Export(typeof(IHandleEndpointRequests))]
    public class EndpointRequestHandler : 
        IHandleEndpointRequests
    {
        /// <summary>
        /// Gets or sets the controller metadata
        /// </summary>
        [Import]
        public IProvideEndpointMethods Endpoints { get; set; }

        /// <summary>
        /// Gets or sets the method executor
        /// </summary>
        [Import]
        public IExecuteEndpointMethods Method { get; set; }

        /// <summary>
        /// Gets or sets the service responder
        /// </summary>
        [Import]
        public IRespondWith RespondWith { get; set; }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>the server response from the method call</returns>
        public async Task<IServerResponse> Handle(IServerRequest request)
        {
            return await SafeActions.Try(() => HandleRun(request), x => HandleError());
        }

        /// <summary>
        /// Handles the run.
        /// </summary>
        /// <returns>The run.</returns>
        /// <param name="request">Request.</param>
        public async Task<IServerResponse> HandleRun(IServerRequest request)
        {
            It.IsNull(request)
                .AsGuard<ArgumentNullException>();

            if (request.Verb == WebMethod.Unsupported)
            {
                return await RespondWith.BadRequest();
            }

            var method = Endpoints.GetMethodFor(request.URI, request.Verb);
            if (It.IsNull(method))
            {
                var supportedMethods = Endpoints.GetSupportedWebMethodsFor(request.URI);
                return await RespondWith.MethodNotAllowed(supportedMethods);
            }

            return await Method.Execute(method, request);
        }

        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <returns>The error.</returns>
        public async Task<IServerResponse> HandleError()
        {
            return await RespondWith.BadRequest();
        }
    }
}
