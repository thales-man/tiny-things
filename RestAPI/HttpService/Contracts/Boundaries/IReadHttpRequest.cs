//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// i read http request (information)
    /// </summary>
    public interface IReadHttpRequest
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        IRequestDetail Uri { get; }

        /// <summary>
        /// Gets the method.
        /// </summary>
        WebMethod? Method { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets the accept media types.
        /// </summary>
        IEnumerable<MediaType> AcceptMediaTypes { get; }

        /// <summary>
        /// Gets the accept charsets.
        /// </summary>
        IEnumerable<string> AcceptCharsets { get; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        MediaType? ContentType { get; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        string ContentCharset { get; }

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        string Content { get; }
    }
}
