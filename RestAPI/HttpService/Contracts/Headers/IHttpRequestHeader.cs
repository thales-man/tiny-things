//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i http request header
    /// </summary>
    public interface IHttpRequestHeader
    {

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Compose(string value);

        /// <summary>
        /// Visits the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        void Visit(IWriteHttpRequest request);
    }
}
