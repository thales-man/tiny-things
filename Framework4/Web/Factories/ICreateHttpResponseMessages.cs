// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Net;
using System.Net.Http;

namespace Tiny.Framework.Web.Factories
{
    /// <summary>
    /// I create http response messages.
    /// </summary>
    public interface ICreateHttpResponseMessages
    {
        /// <summary>
        /// Create...
        /// </summary>
        /// <param name="responseCode">the response code.</param>
        /// <param name="responseContent">the response content.</param>
        /// <param name="contentFormat">the response content format.</param>
        /// <returns>the http response message.</returns>
        HttpResponseMessage Create(HttpStatusCode responseCode, byte[] responseContent, string contentFormat);

        /// <summary>
        /// Create...
        /// </summary>
        /// <param name="responseCode">the response code.</param>
        /// <param name="responseContent">the response content.</param>
        /// <param name="contentFormat">the response content format.</param>
        /// <returns>the http response message.</returns>
        HttpResponseMessage Create(HttpStatusCode responseCode, string responseContent, string contentFormat = null);
    }
}
