//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Tiny.Framework.Utility;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace Http.Service.Provider
{
    /// <summary>
    /// the encoding facilities provider
    /// </summary>
    /// <seealso cref="IProvideEncodingFacilities" />
    [Shared]
    [Export(typeof(IProvideEncodingFacilities))]
    public class EncodingFacilitesProvider : IProvideEncodingFacilities
    {
        // TODO: seriously think about the encoding requirements.. 
        // i get the impression the content type default to UTF-8, after the content serialisers

        /// <summary>
        /// The system encoding
        /// </summary>
        private string systemEncoding = "UTF-8";

        /// <summary>
        /// Gets the supported media.
        /// </summary>
        private ICollection<MediaType> supportedMedia => new List<MediaType> { MediaType.JSON, MediaType.XML };

        /// <summary>
        /// Gets the default encoding
        /// </summary>
        public Encoding GetDefault() => Default;

        /// <summary>
        /// Gets the default encoding
        /// </summary>
        public Encoding Default => Encoding.GetEncoding("ISO-8859-1");

        /// <summary>
        /// Gets the space for the default encoding
        /// </summary>
        public byte Space => Default.GetBytes(new[] { ' ' }).First();

        /// <summary>
        /// Gets the carriage return for the default encoding
        /// </summary>
        public byte CarriageReturn => Default.GetBytes(new[] { '\r' }).First();

        /// <summary>
        /// Gets the line feed for the default encoding
        /// </summary>
        public byte LineFeed => Default.GetBytes(new[] { '\n' }).First();

        /// <summary>
        /// Gets the colon for the default encoding
        /// </summary>
        public byte Colon => Default.GetBytes(new[] { ':' }).First();

        /// <summary>
        /// Gets the encoding for the supported media types
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Media type is not supported</exception>
        public string Get(MediaType candidate)
        {
            if (supportedMedia.Contains(candidate))
            {
                return systemEncoding;
            }

            throw new ArgumentException("Media type is not supported");
        }

        /// <summary>
        /// Gets the encoding for the candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns></returns>
        public Encoding Get(string candidate)
        {
            return It.Has(candidate)
                ? TryGetEncoding(candidate)
                : Default;
        }

        /// <summary>
        /// Tries to the get encoding of the candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>the encoding</returns>
        public Encoding TryGetEncoding(string candidate) => 
            SafeActions.Try(() => Encoding.GetEncoding(candidate), () => Default);

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="requestedEncoding">The requested encoding.</param>
        /// <returns>a byte array using the requested encoding</returns>
        public byte[] GetBytes(string source, string requestedEncoding)
        {
            var encoding = Get(requestedEncoding);
            return encoding.GetBytes(source.ToArray());
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="requestedEncoding">The requested encoding.</param>
        /// <returns>the byte array as an encoded string</returns>
        public string GetString(byte[] source, string requestedEncoding)
        {
            var encoding = Get(requestedEncoding);
            return encoding.GetString(source);
        }
    }
}
