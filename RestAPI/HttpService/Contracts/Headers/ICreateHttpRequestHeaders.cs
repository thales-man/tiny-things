//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i create http request headers
    /// </summary>
    public interface ICreateHttpRequestHeaders
    {

        /// <summary>
        /// Creates the specified header name.
        /// </summary>
        /// <param name="headerName">Name of the header.</param>
        /// <param name="headerValue">The header value.</param>
        /// <returns>a http request header</returns>
        IHttpRequestHeader Create(string headerName, string headerValue);
    }
}
