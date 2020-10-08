//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Providers;
using System;
using System.Composition;

namespace Http.Service.ResponseParsers
{

    /// <summary>
    /// the response firstline parser
    /// </summary>
    /// <seealso cref="ResponseParserBase" />
    /// <seealso cref="IParseResponseFirstLine" />
    [Shared]
    [Export(typeof(IParseResponseFirstLine))]
    public class ResponseFirstLineParser : ResponseParserBase, IParseResponseFirstLine
    {
        [Import]
        public ITranslateStatusCodes Codes { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString(IReadHttpResponse response)
        {
            var version = GetHttpVersion(response.Version);
            var status = (int)response.OperationStatus;
            var statusText = Codes.ToHumanReadable(response.OperationStatus);
            return $"{version} {status} {statusText}\r\n";
        }

        /// <summary>
        /// Gets the HTTP version.
        /// </summary>
        /// <param name="httpVersion">The HTTP version.</param>
        /// <returns>a string representing the HTTP version</returns>
        public string GetHttpVersion(Version httpVersion)
        {
            return $"HTTP/{httpVersion.Major}.{httpVersion.Minor}";
        }
    }
}
