//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;

namespace Http.Service.Contract.Providers
{

    /// <summary>
    /// i parse a http response
    /// </summary>
    public interface IParseHttpResponse
    {
        /// <summary>
        /// To the buffer.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>a buffer for the output stream</returns>
        byte[] ToBuffer(IReadHttpResponse response);
    }
}
