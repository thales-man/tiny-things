//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Headers;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// the response header base
    /// </summary>
    /// <seealso cref="IHttpResponseHeader" />
    public abstract class ResponseHeaderBase : IHttpResponseHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; set; }
    }
}
