//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Composition;
using Http.Service.Contract;
using Http.Service.Contract.Headers;
using Tiny.Framework.Container;

namespace Http.Service.Provider
{
    /// <summary>
    /// the request header factory
    /// </summary>
    /// <seealso cref="ICreateHttpRequestHeaders" />
    [Shared]
    [Export(typeof(ICreateHttpRequestHeaders))]
    public class HttpRequestHeaderFactory : ICreateHttpRequestHeaders
    {
        private Dictionary<string, Func<string, IHttpRequestHeader>> _headerTypes;

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        [Import]
        public IResolveTypes Container { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestHeaderFactory"/> class.
        /// </summary>
        public HttpRequestHeaderFactory()
        {
            _headerTypes = new Dictionary<string, Func<string, IHttpRequestHeader>>(StringComparer.OrdinalIgnoreCase)
            {
                [HttpHeaderNames.ContentLength] = Create<IRequestContentLengthHeader>,
                [HttpHeaderNames.AcceptHeader] = Create<IRequestMediaTypeHeader>,
                [HttpHeaderNames.ContentType] = Create<IRequestContentTypeHeader>,
                [HttpHeaderNames.AcceptCharset] = Create<IRequestEncodingHeader>
            };
        }

        /// <summary>
        /// Creates the specified header name.
        /// </summary>
        /// <param name="headerName">Name of the header.</param>
        /// <param name="headerValue">The header value.</param>
        /// <returns>the header</returns>
        public IHttpRequestHeader Create(string headerName, string headerValue)
        {
            if (_headerTypes.ContainsKey(headerName))
            {
                return _headerTypes[headerName](headerValue);
            }

            return Create<IRequestUntypedHeader>(headerValue);
        }

        /// <summary>
        /// Creates the specified header value.
        /// this is the internal call made from the dictionary
        /// </summary>
        /// <typeparam name="T">IHttpRequestHeader</typeparam>
        /// <param name="headerValue">The header value.</param>
        /// <returns>the header</returns>
        public IHttpRequestHeader Create<T>(string headerValue) where T : IHttpRequestHeader
        {
            var header = Container.Resolve<T>();
            header.Compose(headerValue);

            return header;
        }
    }
}
