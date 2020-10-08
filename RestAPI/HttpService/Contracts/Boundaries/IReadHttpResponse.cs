//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Http.Service.Contract.Headers;
using System;
using System.Collections.Generic;
using System.Net;

namespace Http.Service.Contract.Boundaries
{

    /// <summary>
    /// i read http response (information)
    /// </summary>
    public interface IReadHttpResponse
    {

        /// <summary>
        /// Gets the version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets the operation status.
        /// </summary>
        HttpStatusCode OperationStatus { get; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        ICollection<IHttpResponseHeader> Headers { get; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        string ContentCharset { get; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        string Body { get; }
    }
}
