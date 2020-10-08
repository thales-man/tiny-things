//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using Tiny.API.Contracts;

namespace Tiny.API
{
    /// <summary>
    /// i respond with...
    /// </summary>
    /// <seealso cref="IRespondWith" />
    [Shared]
    [Export(typeof(IRespondWith))]
    public class ResponseFactory : IRespondWith
    {
        /// <summary>
        /// Bad request.
        /// </summary>
        /// <returns>
        /// a server response
        /// </returns>
        public async Task<IServerResponse> BadRequest() =>
            await Task.Run(() => new OperationResult { OperationStatus = HttpStatusCode.BadRequest });

        /// <summary>
        /// Method not allowed.
        /// </summary>
        /// <param name="supportedVerbs">The supported verbs.</param>
        /// <returns>
        /// a server response
        /// </returns>
        public async Task<IServerResponse> MethodNotAllowed(IEnumerable<WebMethod> supportedVerbs) =>
            await Task.Run(() => new OperationResult
            {
                OperationStatus = HttpStatusCode.MethodNotAllowed,
                Result = supportedVerbs,
                ResultType = typeof(IEnumerable<WebMethod>)
            });

        /// <summary>
        /// Responds with a result containing the specified state
        /// </summary>
        /// <param name="operationState">State of the operation.</param>
        /// <returns>
        /// a server response
        /// </returns>
        public async Task<IServerResponse> Result(HttpStatusCode operationState) =>
            await Task.Run(() => new OperationResult { OperationStatus = operationState });

        /// <summary>
        /// Responds with a result containing the specified state and contents
        /// </summary>
        /// <param name="operationState">State of the operation.</param>
        /// <param name="operationResult">The operation result.</param>
        /// <returns>
        /// a server response
        /// </returns>
        public async Task<IServerResponse> Result<TPayload>(HttpStatusCode operationState, TPayload operationResult)
            where TPayload : class =>
            await Task.Run(() => new OperationResult { OperationStatus = operationState, Result = operationResult, ResultType = typeof(TPayload) });

        /// <summary>
        /// Unsupported media type
        /// </summary>
        /// <returns>
        /// a server response
        /// </returns>
        public async Task<IServerResponse> UnsupportedMediaType() =>
            await Task.Run(() => new OperationResult { OperationStatus = HttpStatusCode.UnsupportedMediaType });
    }
}
