//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using Http.Service.Contract.Sets;
using Http.Service.Contract.Headers;
using System.Composition;
using Http.Service.Contract;

namespace Http.Service.Model.Headers.Response
{

    /// <summary>
    /// a response supported verbs header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseSupportedVerbsHeader" />
    [Export(typeof(IResponseSupportedVerbsHeader))]
    public class ResponseSupportedVerbsHeader : ResponseHeaderBase, IResponseSupportedVerbsHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.Allow;

        /// <summary>
        /// Composes the specified supported verbs.
        /// </summary>
        /// <param name="supportedVerbs">The supported verbs.</param>
        public void Compose(IEnumerable<WebMethod> supportedVerbs)
        {
            Value = string.Join(";", supportedVerbs);
        }
    }
}
