// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Net;
using System.Net.Http;
using Tiny.Framework.Registration;
using Tiny.Framework.Web.Helpers;

namespace Tiny.Framework.Web.Factories
{
    /// <summary>
    /// the http response message factory (implementation).
    /// </summary>
    internal sealed class HttpResponseMessageFactory :
        ICreateHttpResponseMessages,
        ISupportServiceRegistration
    {
        /// <inheritdoc/>
        public HttpResponseMessage Create(HttpStatusCode responseCode, byte[] responseContent, string contentFormat) =>
            new HttpResponseMessage(responseCode).SetContent(responseContent, contentFormat);

        /// <inheritdoc/>
        public HttpResponseMessage Create(HttpStatusCode responseCode, string responseContent, string contentFormat = null) =>
            new HttpResponseMessage(responseCode).SetContent(responseContent, contentFormat);
    }
}
