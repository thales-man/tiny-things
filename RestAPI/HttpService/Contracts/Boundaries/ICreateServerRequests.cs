//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// i create server requests
    /// </summary>
    public interface ICreateServerRequests
    {
        /// <summary>
        /// Creates the specified HTTP request.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>a server response</returns>
        IServerRequest Create(IReadHttpRequest httpRequest);
    }
}
