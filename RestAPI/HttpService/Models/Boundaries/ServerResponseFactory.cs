//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;

namespace Http.Service.Model.Boundaries
{
    /// <summary>
    /// i create server responses
    /// i'm not convinced about having this here
    /// </summary>
    /// <seealso cref="ICreateServerResponses" />
    [Shared]
    [Export(typeof(ICreateServerResponses))]
    public class ServerResponseFactory : ICreateServerResponses
    {
        /// <summary>
        /// Gets or sets the header factory.
        /// </summary>
        [Import]
        public ICreateHttpResponseHeaders Header { get; set; }

        /// <summary>
        /// Gets or sets the serializer.
        /// </summary>
        [Import]
        public INegotiateContentSerialization Serialize { get; set; }

        /// <summary>
        /// Gets or sets the media translator.
        /// </summary>
        [Import]
        public ITranslateMediaTypes TranslateMedia { get; set; }

        /// <summary>
        /// Creates the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="request">The request.</param>
        /// <returns>a http response</returns>
        public IReadHttpResponse Create(IServerResponse response, IServerRequest request)
        {
            var content = string.Empty;

            if (response.OperationStatus != HttpStatusCode.MethodNotAllowed)
            {
                content = Serialize.ToString(response.Result, response.ResultType, request.AcceptMediaType);
            }

            var httpResponse = new ServerResponse
            {
                Version = new Version(1, 1),
                OperationStatus = response.OperationStatus,
                ContentCharset = request.AcceptCharset,
                Body = content
            };

            if (response.OperationStatus == HttpStatusCode.MethodNotAllowed)
            {
                var allowed = response.Result as IEnumerable<WebMethod>;
                if (It.HasValues(allowed))
                {
                    httpResponse.AddHeader(Header.Create(HttpHeaderNames.Allow, allowed));
                }
            }

            httpResponse.AddHeader(Header.Create(HttpHeaderNames.Server, string.Empty));
            httpResponse.AddHeader(Header.Create(HttpHeaderNames.Date, DateTime.Now));
            httpResponse.AddHeader(Header.Create(HttpHeaderNames.Connection, "close"));

            if (It.Has(content))
            {
                httpResponse.AddHeader(Header.Create(HttpHeaderNames.ContentLength, content.Length + 2));
                httpResponse.AddHeader(Header.Create(HttpHeaderNames.ContentType, FormatEncoding(request.AcceptMediaType, request.AcceptCharset)));
            }

            return httpResponse;
        }

        /// <summary>
        /// Formats the encoding.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="charset">The charset.</param>
        /// <returns>a formatted encoding</returns>
        public string FormatEncoding(MediaType? mediaType, string charset)
        {
            if (It.Has(mediaType) && It.Has(charset))
            {
                var contentType = TranslateMedia.ToString(mediaType.Value);
                string charsetPart = $";{HttpHeaderNames.Charset}={charset}";
                return string.Concat(contentType, charsetPart);
            }
            return string.Empty;
        }
    }
}
