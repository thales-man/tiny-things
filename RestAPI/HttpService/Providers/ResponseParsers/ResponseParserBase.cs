//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Providers;
using System.Composition;

namespace Http.Service.ResponseParsers
{

    /// <summary>
    /// the response parser base abstract class
    /// </summary>
    /// <seealso cref="IParseAResponsePart" />
    public abstract class ResponseParserBase : IParseAResponsePart
    {

        /// <summary>
        /// Gets or sets the encoder (used in most of the parsers)
        /// </summary>
        [Import]
        public IProvideEncodingFacilities Encoder { get; set; }

        /// <summary>
        /// To the bytes.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>a byte representation of this part of the response</returns>
        public virtual byte[] ToBytes(IReadHttpResponse response)
        {
            return Encoder.GetBytes(ToString(response));
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public abstract string ToString(IReadHttpResponse response);
    }
}
