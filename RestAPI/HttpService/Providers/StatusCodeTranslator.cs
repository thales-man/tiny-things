//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Providers;
using System.Composition;
using System.Net;
using Tiny.Framework.Utility;

namespace Http.Service.Provider
{

    /// <summary>
    /// i translate response codes
    /// </summary>
    /// <seealso cref="ITranslateStatusCodes" />
    [Shared]
    [Export(typeof(ITranslateStatusCodes))]
    public class StatusCodeTranslator : ITranslateStatusCodes
    {

        /// <summary>
        /// To a human readable (string)
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <returns>
        /// human readable text for the status code
        /// </returns>
        public string ToHumanReadable(HttpStatusCode responseCode)
        {
            return FromSet<HttpStatusCode>.ToText(responseCode);
        }
    }
}
