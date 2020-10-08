//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a request content type header
    /// </summary>
    /// <seealso cref="HttpQuantifiedHeaderBase" />
    /// <seealso cref="IRequestContentTypeHeader" />
    [Export(typeof(IRequestContentTypeHeader))]
    public class RequestContentTypeHeader : HttpQuantifiedHeaderBase, IRequestContentTypeHeader
    {

        /// <summary>
        /// Gets or sets the translate media.
        /// </summary>
        [Import]
        public ITranslateMediaTypes TranslateMedia { get; set; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        public MediaType ContentType { get; private set; }

        /// <summary>
        /// Gets the content charset.
        /// </summary>
        public string ContentCharset { get; private set; }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            var quantifiedHeaderValue = ExtractQuantifiedHeader(value);
            ContentCharset = quantifiedHeaderValue.FindQuantifierValue(HttpHeaderNames.Charset);
            ContentType = TranslateMedia.ToMediaType(quantifiedHeaderValue.HeaderValue);
        }

        /// <summary>
        /// Visits the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Visit(IWriteHttpRequest writer)
        {
            writer.SetContentEncoding(ContentCharset);
            writer.SetContentType(ContentType);
        }
    }
}
