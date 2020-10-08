//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Text;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;

namespace Http.Service.Model.Boundaries
{
    /// <summary>
    /// an incoming server request
    /// </summary>
    /// <seealso cref="IServerRequest" />
    public class ServerRequest : IServerRequest
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        public IRequestDetail URI { get; set; }

        /// <summary>
        /// Gets the web method (verb).
        /// </summary>
        public WebMethod Verb { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets the accept charset.
        /// </summary>
        public string AcceptCharset { get; set; }

        /// <summary>
        /// Gets the type of the accept media.
        /// </summary>
        public MediaType AcceptMediaType { get; set; }

        /// <summary>
        /// Gets the accept encoding.
        /// </summary>
        public Encoding AcceptEncoding { get; set; }

        /// <summary>
        /// Gets the type of the content media.
        /// </summary>
        public MediaType ContentMediaType { get; set; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        public string ContentCharset { get; set; }

        /// <summary>
        /// Gets the content encoding.
        /// </summary>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        public string Body { get; set; }
    }
}
