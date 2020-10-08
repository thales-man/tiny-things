//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Sets;
using System.Text;

namespace Http.Service.Contract.Providers
{

    /// <summary>
    /// i provide encoding facilities
    /// </summary>
    public interface IProvideEncodingFacilities
    {

        /// <summary>
        /// Gets the default.
        /// </summary>
        Encoding Default { get; }

        /// <summary>
        /// Gets the space.
        /// </summary>
        byte Space { get; }

        /// <summary>
        /// Gets the carriage return.
        /// </summary>
        byte CarriageReturn { get; }

        /// <summary>
        /// Gets the line feed.
        /// </summary>
        byte LineFeed { get; }

        /// <summary>
        /// Gets the colon.
        /// </summary>
        byte Colon { get; }

        /// <summary>
        /// Gets the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>teh message encoding string</returns>
        string Get(MediaType candidate);

        /// <summary>
        /// Gets the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>the message encoding</returns>
        Encoding Get(string candidate);

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="requestedEncoding">The requested encoding.</param>
        /// <returns>a byte array</returns>
        byte[] GetBytes(string source, string requestedEncoding = null);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="requestedEncoding">The requested encoding.</param>
        /// <returns>a string</returns>
        string GetString(byte[] source, string requestedEncoding = null);
    }
}
