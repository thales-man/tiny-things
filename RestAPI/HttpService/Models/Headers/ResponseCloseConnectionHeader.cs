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
    /// a response close connection header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseCloseConnectionHeader" />
    [Export(typeof(IResponseCloseConnectionHeader))]
    public class ResponseCloseConnectionHeader : ResponseHeaderBase, IResponseCloseConnectionHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCloseConnectionHeader"/> class.
        /// </summary>
        public ResponseCloseConnectionHeader() { }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            Value = value;
        }
    }
}
