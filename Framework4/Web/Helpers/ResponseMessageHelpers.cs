// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tiny.Framework.Web.Helpers
{
    /// <summary>
    /// HTTP response message helpers.
    /// </summary>
    public static class ResponseMessageHelpers
    {
        /// <summary>
        /// the (required) response content type.
        /// </summary>
        public const string ResponseContentType = "application/json";

        /// <summary>
        /// Set the content of the response.
        /// </summary>
        /// <param name="source">the source.</param>
        /// <param name="content">the content.</param>
        /// <param name="contentFormat">the content format.</param>
        /// <returns>the message with content.</returns>
        public static HttpResponseMessage SetContent(this HttpResponseMessage source, byte[] content, string contentFormat)
        {
            source.Content = new ByteArrayContent(content);
            source.Content.Headers.Remove("Content-Type");
            source.Content.Headers.Add("Content-Type", contentFormat);

            return source;
        }

        /// <summary>
        /// Set the content of the response.
        /// </summary>
        /// <param name="source">the source.</param>
        /// <param name="content">the content.</param>
        /// <param name="contentFormat">the content format.</param>
        /// <returns>the message with content.</returns>
        public static HttpResponseMessage SetContent(this HttpResponseMessage source, string content, string contentFormat)
        {
            source.Content = new StringContent(content, Encoding.UTF8, contentFormat ?? ResponseContentType);
            return source;
        }

        /// <summary>
        /// As action result...
        /// </summary>
        /// <param name="source">the source.</param>
        /// <returns>An MVC action result.</returns>
        public static async Task<IActionResult> AsActionResult(this Task<HttpResponseMessage> source)
        {
            var response = await source;
            return new HttpResponseMessageResult(response);
        }
    }
}