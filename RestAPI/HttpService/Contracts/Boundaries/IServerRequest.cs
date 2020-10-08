//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Text;
using Http.Service.Contract.Sets;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// an incoming server request
    /// </summary>
    public interface IServerRequest
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        IRequestDetail URI { get; }

        /// <summary>
        /// Gets the web method (verb).
        /// </summary>
        WebMethod Verb { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets the accept charset.
        /// </summary>
        string AcceptCharset { get; }

        /// <summary>
        /// Gets the type of the accept media.
        /// </summary>
        MediaType AcceptMediaType { get; }

        /// <summary>
        /// Gets the accept encoding.
        /// </summary>
        Encoding AcceptEncoding { get; }

        /// <summary>
        /// Gets the type of the content media.
        /// </summary>
        MediaType ContentMediaType { get; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        string ContentCharset { get; }

        /// <summary>
        /// Gets the content encoding.
        /// </summary>
        Encoding ContentEncoding { get; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        string Body { get; }
    }
}
