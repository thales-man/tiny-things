//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Http.Service.Contract.Headers;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// i write http request (information)
    /// </summary>
    public interface IWriteHttpRequest
    {

        /// <summary>
        /// Sets the web method.
        /// </summary>
        /// <param name="Method">The method.</param>
        void SetWebMethod(WebMethod Method);

        /// <summary>
        /// Sets the media types.
        /// </summary>
        /// <param name="mediaTypes">The media types.</param>
        void SetMediaTypes(IEnumerable<MediaType> mediaTypes);

        /// <summary>
        /// Sets the character sets.
        /// </summary>
        /// <param name="charsets">The charsets.</param>
        void SetCharSets(IEnumerable<string> charsets);

        /// <summary>
        /// Sets the URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        void SetURI(IRequestDetail uri);

        /// <summary>
        /// Sets the content encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        void SetContentEncoding(string encoding);

        /// <summary>
        /// Sets the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        void SetContentType(MediaType contentType);

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="httpVersion">The HTTP version.</param>
        void SetVersion(Version httpVersion);

        /// <summary>
        /// Sets the length of the content.
        /// </summary>
        /// <param name="length">The length.</param>
        void SetContentLength(int length);

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="content">The content.</param>
        void SetContent(string content);

        /// <summary>
        /// Adds the header.
        /// </summary>
        /// <param name="header">The header.</param>
        void AddHeader(IHttpRequestHeader header);
    }
}
