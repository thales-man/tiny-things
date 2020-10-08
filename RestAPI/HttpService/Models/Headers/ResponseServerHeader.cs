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
    /// a response server header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseServerHeader" />
    [Export(typeof(IResponseServerHeader))]
    public class ResponseServerHeader : ResponseHeaderBase, IResponseServerHeader
    {

        /// <summary>
        /// The API server version
        /// </summary>
        const string APIServerVersion = "Tiny API (v1.0 PCL)";

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.Server;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseServerHeader"/> class.
        /// </summary>
        public ResponseServerHeader() { }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            Value = APIServerVersion;
        }
    }
}
