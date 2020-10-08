//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// Request detail.
    /// </summary>
    public interface IRequestDetail
    {
        /// <summary>
        /// Gets the original.
        /// </summary>
        /// <value>The original.</value>
        string Original { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>The query.</value>
        string Query { get; }

        /// <summary>
        /// Gets the query parameters.
        /// </summary>
        /// <value>The query parameters.</value>
        IDictionary<string, string> QueryParameters { get; }
    }
}
