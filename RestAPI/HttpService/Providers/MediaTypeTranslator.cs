//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using System.Collections.Generic;
using System.Composition;

namespace Http.Service.Provider
{

    /// <summary>
    /// i translate media types
    /// </summary>
    /// <seealso cref="ITranslateMediaTypes" />
    [Shared]
    [Export(typeof(ITranslateMediaTypes))]
    public class MediaTypeTranslator : ITranslateMediaTypes
    {
        private readonly IDictionary<MediaType, string> _mediaTypeToText = new Dictionary<MediaType, string>()
        {
            [MediaType.Unsupported] = string.Empty,
            [MediaType.JSON] = "application/json",
            [MediaType.XML] = "application/xml"
        };
        private readonly IDictionary<string, MediaType> _textToMediaType = new Dictionary<string, MediaType>()
        {
            ["application/json"] = MediaType.JSON,
            ["text/json"] = MediaType.JSON,
            ["application/xml"] = MediaType.XML,
            ["text/xml"] = MediaType.XML,
        };

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(MediaType mediaType)
        {
            return _mediaTypeToText[mediaType];
        }

        /// <summary>
        /// To the type of media.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the media type</returns>
        public MediaType ToMediaType(string value)
        {
            return _textToMediaType.ContainsKey(value)
                ? _textToMediaType[value]
                : MediaType.Unsupported;
        }
    }
}
