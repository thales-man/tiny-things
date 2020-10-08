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
    /// the response content parser
    /// </summary>
    /// <seealso cref="ResponseParserBase" />
    /// <seealso cref="IParseResponseBody" />
    [Shared]
    [Export(typeof(IParseResponseBody))]
    public class ResponseBodyParser : ResponseParserBase, IParseResponseBody
    {

        /// <summary>
        /// To the bytes.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>an locale encoded byte array for content string</returns>
        public override byte[] ToBytes(IReadHttpResponse response)
        {
            return Encoder.GetBytes(ToString(response), response.ContentCharset);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString(IReadHttpResponse response)
        {
            return $"\r\n{response.Body}";
        }
    }
}
