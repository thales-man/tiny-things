//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Sets;

namespace Http.Service.Contract.Providers
{

    /// <summary>
    /// i translate media types
    /// </summary>
    public interface ITranslateMediaTypes
    {

        /// <summary>
        /// To the type of the media.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns>a media type</returns>
        MediaType ToMediaType(string mediaType);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ToString(MediaType mediaType);
    }
}
