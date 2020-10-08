//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Tiny.Framework.Utility;

namespace Http.Service.Provider
{

    /// <summary>
    /// the http server request
    /// </summary>
    /// <seealso cref="IHttpRequest" />
    [Export(typeof(IHttpRequest))]
    public sealed class HttpServerRequest : IHttpRequest
    {

        /// <summary>
        /// The _headers
        /// </summary>
        private ICollection<IHttpRequestHeader> _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServerRequest"/> class.
        /// </summary>
        public HttpServerRequest()
        {
            _headers = Collection.Empty<IHttpRequestHeader>();
            AcceptCharsets = Enumerable.Empty<string>();
            AcceptMediaTypes = Enumerable.Empty<MediaType>();
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        public IEnumerable<IHttpRequestHeader> Headers => _headers;

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public WebMethod? Method { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        public IRequestDetail Uri { get; set; }

        /// <summary>
        /// Gets or sets the accept media types.
        /// </summary>
        public IEnumerable<MediaType> AcceptMediaTypes { get; set; }

        /// <summary>
        /// Gets or sets the accept charsets.
        /// </summary>
        public IEnumerable<string> AcceptCharsets { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        public MediaType? ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content charset.
        /// </summary>
        public string ContentCharset { get; set; }

        /// <summary>
        /// Gets or sets the length of the content.
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Adds the header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <exception cref="InvalidOperationException">can't add header once processing request is finished!</exception>
        public void AddHeader(IHttpRequestHeader header)
        {
            header.Visit(this);
            _headers.Add(header);
        }

        /// <summary>
        /// Sets the media types.
        /// </summary>
        /// <param name="mediaTypes">The media types.</param>
        public void SetMediaTypes(IEnumerable<MediaType> mediaTypes)
        {
            AcceptMediaTypes = mediaTypes;
        }

        /// <summary>
        /// Sets the length of the content.
        /// </summary>
        /// <param name="length">The length.</param>
        public void SetContentLength(int length)
        {
            ContentLength = length;
        }

        /// <summary>
        /// Sets the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public void SetContentType(MediaType contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// Sets the character sets.
        /// </summary>
        /// <param name="charsets">The charsets.</param>
        public void SetCharSets(IEnumerable<string> charsets)
        {
            AcceptCharsets = charsets;
        }

        /// <summary>
        /// Sets the URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public void SetURI(IRequestDetail uri)
        {
            Uri = uri;
        }

        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <param name="method">The method.</param>
        public void SetMethod(WebMethod method)
        {
            Method = method;
        }

        /// <summary>
        /// Appends the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the append content result</returns>
        public void SetContent(string content)
        {
            Content = content;
        }

        /// <summary>
        /// Sets the web method.
        /// </summary>
        /// <param name="webMethod">The web method.</param>
        public void SetWebMethod(WebMethod webMethod)
        {
            Method = webMethod;
        }

        /// <summary>
        /// Sets the content encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public void SetContentEncoding(string encoding)
        {
            ContentCharset = encoding;
        }

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="version">The version.</param>
        public void SetVersion(Version version)
        {
            Version = version;
        }
    }
}
