//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a request media type header
    /// </summary>
    /// <seealso cref="Http.Service.Model.Headers.HttpQuantifiedHeaderBase" />
    /// <seealso cref="Http.Service.Contract.Headers.IRequestMediaTypeHeader" />
    [Export(typeof(IRequestMediaTypeHeader))]
    public class RequestMediaTypeHeader : HttpQuantifiedHeaderBase, IRequestMediaTypeHeader
    {

        /// <summary>
        /// Gets or sets the type translator.
        /// </summary>
        [Import]
        public ITranslateMediaTypes TypeTranslator { get; set; }

        /// <summary>
        /// Gets or sets the accept types.
        /// </summary>
        public IEnumerable<MediaType> AcceptTypes { get; set; }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            var quantifiedHeaderValues = ExtractQuantifiedHeaders(value);
            AcceptTypes = quantifiedHeaderValues.Select(q => TypeTranslator.ToMediaType(q.HeaderValue));
        }

        /// <summary>
        /// Visits the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Visit(IWriteHttpRequest writer)
        {
            writer.SetMediaTypes(AcceptTypes);
        }
    }
}
