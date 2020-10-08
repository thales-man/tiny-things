//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System.Composition;
using System.Linq;

namespace Http.Service.Model.Boundaries
{
    /// <summary>
    /// i create server requests
    /// i'm not convinced about having this here
    /// </summary>
    /// <seealso cref="ICreateServerRequests" />
    [Shared]
    [Export(typeof(ICreateServerRequests))]
    public class ServerRequestFactory : ICreateServerRequests
    {
        /// <summary>
        /// Gets or sets the encoder.
        /// </summary>
        [Import]
        public IProvideEncodingFacilities Encoder { get; set; }

        /// <summary>
        /// Creates the specified HTTP request.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>a final server request, built from the candidate http request</returns>
        public IServerRequest Create(IReadHttpRequest httpRequest)
        {
            var acceptMediaType = GetAcceptMediaType(httpRequest);
            var acceptCharset = GetAcceptCharset(httpRequest, acceptMediaType);
            var contentMediaType = GetContentMediaType(httpRequest);
            var contentCharset = GetContentCharset(httpRequest, contentMediaType);

            return new ServerRequest
            {
                Verb = httpRequest.Method.Value,
                URI = httpRequest.Uri,
                AcceptCharset = acceptCharset,
                AcceptMediaType = acceptMediaType,
                AcceptEncoding = Encoder.Get(acceptCharset),
                ContentMediaType = contentMediaType,
                ContentCharset = contentCharset,
                ContentEncoding = Encoder.Get(contentCharset),
                Body = httpRequest.Content,
                Version = httpRequest.Version
            };
        }

        /// <summary>
        /// Gets the type of the content media.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>a media type</returns>
        private MediaType GetContentMediaType(IReadHttpRequest httpRequest)
        {
            return httpRequest.ContentType.GetValueOrDefault() == MediaType.Unsupported
                ? MediaType.XML
                : httpRequest.ContentType.Value;
        }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <returns>the content encoding</returns>
        private string GetContentCharset(IReadHttpRequest httpRequest, MediaType contentMediaType)
        {
            var contentEncoding = httpRequest.ContentCharset;
            if (Encoder.Get(contentEncoding) == null)
            {
                contentEncoding = Encoder.Get(contentMediaType);
            }

            return contentEncoding;
        }

        /// <summary>
        /// Gets the type of the accept media.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>the accept media type</returns>
        private MediaType GetAcceptMediaType(IReadHttpRequest httpRequest)
        {
            var preferredType = httpRequest.AcceptMediaTypes.FirstOrDefault(a => a != MediaType.Unsupported);
            if (preferredType == MediaType.Unsupported)
            {
                preferredType = MediaType.XML;
            }

            return preferredType;
        }

        /// <summary>
        /// Gets the accept charset.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="acceptMediaType">Type of the accept media.</param>
        /// <returns>the accept charset</returns>
        private string GetAcceptCharset(IReadHttpRequest httpRequest, MediaType acceptMediaType)
        {
            var acceptEncoding = httpRequest.AcceptCharsets.FirstOrDefault(x => It.Has(Encoder.Get(x)));
            if (It.IsEmpty(acceptEncoding))
            {
                acceptEncoding = Encoder.Get(acceptMediaType);
            }

            return acceptEncoding;
        }
    }
}
