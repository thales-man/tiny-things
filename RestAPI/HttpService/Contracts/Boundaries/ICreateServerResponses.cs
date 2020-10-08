//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// i create server responses
    /// </summary>
    public interface ICreateServerResponses
    {
        /// <summary>
        /// Creates the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="request">The request.</param>
        /// <returns>a read http response</returns>
        IReadHttpResponse Create(IServerResponse response, IServerRequest request);
    }
}
