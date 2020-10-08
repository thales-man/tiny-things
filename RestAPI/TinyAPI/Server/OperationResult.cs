//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using System;
using System.Net;

namespace Tiny.API
{
    /// <summary>
    /// the real 'method' response
    /// </summary>
    /// <seealso cref="IServerResponse" />
    public class OperationResult : IServerResponse
    {
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        public HttpStatusCode OperationStatus { get; set; }

        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        public Type ResultType { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public object Result { get; set; }
    }
}
