//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Net;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// an outgoing server response
    /// </summary>
    public interface IServerResponse
    {
        /// <summary>
        /// Gets the operation status.
        /// </summary>
        HttpStatusCode OperationStatus { get; }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        Type ResultType { get; }

        /// <summary>
        /// Gets the operation status.
        /// </summary>
        object Result { get; }
    }
}
