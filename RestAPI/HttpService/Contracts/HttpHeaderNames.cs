//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract
{

    /// <summary>
    /// the http header names
    /// </summary>
    public struct HttpHeaderNames
    {

        /// <summary>
        /// The content length
        /// </summary>
        public const string ContentLength = "Content-Length";

        /// <summary>
        /// The accept header
        /// </summary>
        public const string AcceptHeader = "Accept";

        /// <summary>
        /// The content type
        /// </summary>
        public const string ContentType = "Content-Type";

        /// <summary>
        /// The accept charset
        /// </summary>
        public const string AcceptCharset = "Accept-Charset";

        /// <summary>
        /// The charset
        /// </summary>
        public const string Charset = "charset";

        /// <summary>
        /// The date
        /// </summary>
        public const string Date = "Date";

        /// <summary>
        /// The allow
        /// </summary>
        public const string Allow = "Allow";

        /// <summary>
        /// The connection
        /// </summary>
        public const string Connection = "Connection";

        /// <summary>
        /// The location
        /// </summary>
        public const string Location = "Location";

        /// <summary>
        /// The server
        /// </summary>
        public const string Server = "Server";
    }
}
