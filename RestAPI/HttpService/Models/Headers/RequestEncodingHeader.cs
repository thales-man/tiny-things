//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a request encoding header
    /// </summary>
    /// <seealso cref="HttpQuantifiedHeaderBase" />
    /// <seealso cref="IRequestEncodingHeader" />
    [Export(typeof(IRequestEncodingHeader))]
    public class RequestEncodingHeader : HttpQuantifiedHeaderBase, IRequestEncodingHeader
    {

        /// <summary>
        /// Gets the supported character sets.
        /// </summary>
        public IEnumerable<string> SupportedCharacterSets { get; private set; }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            var quantifiedHeaderValues = ExtractQuantifiedHeaders(value);
            SupportedCharacterSets = quantifiedHeaderValues.Select(q => q.HeaderValue);
        }

        /// <summary>
        /// Visits the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Visit(IWriteHttpRequest writer)
        {
            writer.SetCharSets(SupportedCharacterSets);
        }
    }
}
