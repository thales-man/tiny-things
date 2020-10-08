//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Composition;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Providers;
using Tiny.Framework.Utility;

namespace Http.Service.ResponseParsers
{

    /// <summary>
    /// the response to byte array / buffer converter
    /// </summary>
    /// <seealso cref="IParseHttpResponse" />
    [Shared]
    [Export(typeof(IParseHttpResponse))]
    public class HttpResponseParser : IParseHttpResponse
    {

        /// <summary>
        /// Gets or sets the first line.
        /// </summary>
        [Import]
        public IParseResponseFirstLine FirstLine { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        [Import]
        public IParseResponseHeaders Headers { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Import]
        public IParseResponseBody Content { get; set; }

        /// <summary>
        /// Gets the pipeline.
        /// </summary>
        /// <returns>a list of response part parsers</returns>
        public List<IParseAResponsePart> GetPipeline()
        {
            return new List<IParseAResponsePart> { FirstLine, Headers, Content };
        }

        /// <summary>
        /// Converts to bytes.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>the response as a byte array</returns>
        public byte[] ConvertToBytes(IReadHttpResponse response)
        {
            var responseValue = Array.Empty<byte>();
            var pipeLine = GetPipeline();
            pipeLine
                .ForEach(x => responseValue = ArrayBlock.Combine(responseValue, x.ToBytes(response)));

            return responseValue;
        }

        /// <summary>
        /// Converts the response to a buffer.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>the buffer</returns>
        public byte[] ToBuffer(IReadHttpResponse response)
        {
            return ConvertToBytes(response);
        }
    }
}
