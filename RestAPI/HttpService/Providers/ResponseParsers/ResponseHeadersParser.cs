//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using System.Composition;

namespace Http.Service.ResponseParsers
{

    /// <summary>
    /// the response headers parser
    /// </summary>
    /// <seealso cref="ResponseParserBase" />
    /// <seealso cref="IParseResponseHeaders" />
    [Shared]
    [Export(typeof(IParseResponseHeaders))]
    public class ResponseHeadersParser : ResponseParserBase, IParseResponseHeaders
    {

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString(IReadHttpResponse response)
        {
            var headers = string.Empty;
            foreach (var header in response.Headers)
            {
                headers += $"{header.Name}: {header.Value}\r\n";
            }
            return headers;
        }
    }
}
