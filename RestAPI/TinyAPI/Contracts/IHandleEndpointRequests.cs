//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using System.Threading.Tasks;

namespace Tiny.API.Contracts
{

    /// <summary>
    /// i handle controller requests
    /// </summary>
    public interface IHandleEndpointRequests
    {

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a server response</returns>
        Task<IServerResponse> Handle(IServerRequest request);
    }
}
