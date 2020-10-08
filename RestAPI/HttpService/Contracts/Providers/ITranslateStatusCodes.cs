//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Net;

namespace Http.Service.Contract.Providers
{

    /// <summary>
    /// i translate http status codes
    /// </summary>
    public interface ITranslateStatusCodes
    {

        /// <summary>
        /// To a human readable (string)
        /// </summary>
        /// <param name="responseCode">The response code.</param>
        /// <returns>human readable text for the status code</returns>
        string ToHumanReadable(HttpStatusCode responseCode);
    }
}
