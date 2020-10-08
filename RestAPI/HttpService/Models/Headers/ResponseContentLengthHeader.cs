//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract;
using Http.Service.Contract.Headers;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a response content length header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseContentLengthHeader" />
    [Export(typeof(IResponseContentLengthHeader))]
    public class ResponseContentLengthHeader : ResponseHeaderBase, IResponseContentLengthHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.ContentLength;

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(int value)
        {
            Value = value.ToString();
        }
    }
}
