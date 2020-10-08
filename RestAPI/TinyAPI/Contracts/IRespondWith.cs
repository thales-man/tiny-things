//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Tiny.API.Contracts
{

    /// <summary>
    /// i responsd with (server responses)
    /// </summary>
    public interface IRespondWith
    {

        /// <summary>
        /// Bad request.
        /// </summary>
        /// <returns>a server response</returns>
        Task<IServerResponse> BadRequest();

        /// <summary>
        /// Method not allowed.
        /// </summary>
        /// <param name="supportedVerbs">The supported verbs.</param>
        /// <returns>a server response</returns>
        Task<IServerResponse> MethodNotAllowed(IEnumerable<WebMethod> supportedVerbs);

        /// <summary>
        /// Unsupported media type 
        /// </summary>
        /// <returns>a server response</returns>
        Task<IServerResponse> UnsupportedMediaType();

        /// <summary>
        /// Responds with a result containing the specified state
        /// </summary>
        /// <param name="operationState">State of the operation.</param>
        /// <returns>a server response</returns>
        Task<IServerResponse> Result(HttpStatusCode operationState);

        /// <summary>
        /// Responds with a result containing the specified state and contents
        /// </summary>
        /// <param name="operationState">State of the operation.</param>
        /// <param name="operationResult">The operation result.</param>
        /// <returns>a server response</returns>
        Task<IServerResponse> Result<TPayload>(HttpStatusCode operationState, TPayload operationResult)
            where TPayload : class;
    }
}
