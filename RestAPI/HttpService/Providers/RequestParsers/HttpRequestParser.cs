//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Providers;

namespace Http.Service.Provider.RequestParsers
{
    /// <summary>
    /// the incoming request parsers
    /// produces a 'low level' request class
    /// </summary>
    /// <seealso cref="IParseHttpRequest" />
    [Shared]
    [Export(typeof(IParseHttpRequest))]
    public class HttpRequestParser : IParseHttpRequest
    {
        private static readonly string[] bodySplitter = { "\r\n\r\n" };

        private static readonly string[] headerSplitter = { "\r\n" };

        /// <summary>
        /// Gets or sets the method parser.
        /// </summary>
        [Import]
        public IParseRequestFirstLine ParseFirstLine { get; set; }

        /// <summary>
        /// Gets or sets the headers parser.
        /// </summary>
        [Import]
        public IParseRequestHeaders ParseHeader { get; set; }

        /// <summary>
        /// Gets or sets the content parser.
        /// </summary>
        [Import]
        public IParseRequestBody ParseBody { get; set; }

        /// <summary>
        /// Gets or sets the stream reader.
        /// </summary>
        [Import]
        public IReadInputStreams StreamReader { get; set; }

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        [Import]
        public ExportFactory<IHttpRequest> Factory { get; set; }

        /// <summary>
        /// Parses the specified request stream.
        /// </summary>
        /// <param name="requestStream">The request stream.</param>
        /// <returns></returns>
        public async Task<IHttpRequest> Parse(IStreamIn requestStream)
        {
            var request = Factory.CreateExport().Value;
            var incoming = await StreamReader.ReadAll(requestStream);
            var requestParts = incoming.Split(bodySplitter, StringSplitOptions.RemoveEmptyEntries);
            var headerParts = requestParts.First().Split(headerSplitter, StringSplitOptions.RemoveEmptyEntries);

            ParseFirstLine.Handle(headerParts.First(), request);

            for (var i = 1; i < headerParts.Length; i++)
            {
                ParseHeader.Handle(headerParts[i], request);
            }

            if (requestParts.Length > 1)
            {
                ParseBody.Handle(requestParts[1], request);
            }

            return request;
        }
    }
}
