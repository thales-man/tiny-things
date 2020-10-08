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
    /// the response header factory
    /// </summary>
    /// <seealso cref="ICreateHttpResponseHeaders" />
    [Shared]
    [Export(typeof(ICreateHttpResponseHeaders))]
    public class HttpResponseHeaderFactory : ICreateHttpResponseHeaders
    {

        /// <summary>
        /// The _header types
        /// </summary>
        private Dictionary<string, Func<IHttpResponseHeader>> _headerTypes;

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        [Import]
        public IResolveTypes Container { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseHeaderFactory"/> class.
        /// </summary>
        public HttpResponseHeaderFactory()
        {
            _headerTypes = new Dictionary<string, Func<IHttpResponseHeader>>(StringComparer.OrdinalIgnoreCase)
            {
                [HttpHeaderNames.Server] = Create<IResponseServerHeader>,
                [HttpHeaderNames.Date] = Create<IResponseDateHeader>,
                [HttpHeaderNames.ContentLength] = Create<IResponseContentLengthHeader>,
                [HttpHeaderNames.Connection] = Create<IResponseCloseConnectionHeader>,
                [HttpHeaderNames.ContentType] = Create<IResponseEncodingHeader>,
                [HttpHeaderNames.Allow] = Create<IResponseSupportedVerbsHeader>
            };
        }

        /// <summary>
        /// Creates the specified header name.
        /// </summary>
        /// <typeparam name="THeaderTypeValue">The type of the header type value.</typeparam>
        /// <param name="headerName">Name of the header.</param>
        /// <param name="headerValue">The header value.</param>
        /// <returns></returns>
        public IHttpResponseHeader Create<THeaderTypeValue>(string headerName, THeaderTypeValue headerValue)
        {
            if (_headerTypes.ContainsKey(headerName))
            {
                var header = _headerTypes[headerName]() as IHttpResponseHeader<THeaderTypeValue>;
                Compose(header, headerValue);
                return header;
            }
            var header2 = Create<IResponseUntypedHeader>() as IHttpResponseHeader<string[]>;
            Compose(header2, new string[] { headerName, headerValue.ToString() });
            return header2;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <typeparam name="THeaderType">The type of the header type.</typeparam>
        /// <returns></returns>
        private IHttpResponseHeader Create<THeaderType>() where THeaderType : IHttpResponseHeader
        {
            return Container.Resolve<THeaderType>();
        }

        /// <summary>
        /// Composes the specified header.
        /// </summary>
        /// <typeparam name="THeaderTypeValue">The type of the header type value.</typeparam>
        /// <param name="header">The header.</param>
        /// <param name="headerValue">The header value.</param>
        private void Compose<THeaderTypeValue>(IHttpResponseHeader<THeaderTypeValue> header, THeaderTypeValue headerValue)
        {
            header.Compose(headerValue);
        }
    }
}
