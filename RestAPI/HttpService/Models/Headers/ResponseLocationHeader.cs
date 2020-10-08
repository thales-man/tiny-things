//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract;
using Http.Service.Contract.Headers;
using System;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a response location header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseLocationHeader" />
    [Export(typeof(IResponseLocationHeader))]
    public class ResponseLocationHeader : ResponseHeaderBase, IResponseLocationHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.Location;

        /// <summary>
        /// Composes the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        public void Compose(Uri location)
        {
            Value = location.ToString();
        }
    }
}
