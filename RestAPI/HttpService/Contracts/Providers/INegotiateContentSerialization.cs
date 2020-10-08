//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Sets;
using System;

namespace Http.Service.Contract.Providers
{
    /// <summary>
    /// i negotiate content serialisation
    /// </summary>
    public interface INegotiateContentSerialization
    {
        /// <summary>
        /// From string.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>a deserialised object</returns>
        object FromString(string content, Type contentType, MediaType contentMediaType);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="content">The content item.</param>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ToString(object content, Type cotentType, MediaType contentMediaType);
    }
}
