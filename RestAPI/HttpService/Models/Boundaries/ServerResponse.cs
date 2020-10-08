//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using System;
using System.Collections.Generic;
using System.Net;

namespace Http.Service.Model.Boundaries
{

    /// <summary>
    /// a server response
    /// </summary>
    /// <seealso cref="IReadHttpResponse" />
    public class ServerResponse : IReadHttpResponse
    {

        /// <summary>
        /// Gets the headers.
        /// </summary>
        public ICollection<IHttpResponseHeader> Headers { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets the operation status.
        /// </summary>
        public HttpStatusCode OperationStatus { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        public string ContentCharset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponse"/> class.
        /// </summary>
        public ServerResponse()
        {
            Version = new Version(1, 1);
            OperationStatus = HttpStatusCode.OK;
            Headers = Collection.Empty<IHttpResponseHeader>();
        }

        /// <summary>
        /// Adds a header.
        /// </summary>
        /// <param name="newHeader">The new header.</param>
        public void AddHeader(IHttpResponseHeader newHeader)
        {
            Headers.Add(newHeader);
        }
    }
}
