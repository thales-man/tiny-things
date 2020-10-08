// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tiny.Framework.Web.Factories;

namespace Tiny.Framework.Web.Providers
{
    /// <summary>
    /// I provide fault responses (contract).
    /// </summary>
    public interface IProvideFaultResponses
    {
        /// <summary>
        /// Get (the) response for...
        /// </summary>
        /// <param name="exception">the exception.</param>
        /// <param name="loggingScope">the logging scope.</param>
        /// <returns>A http response message with a status and messge for the problem.</returns>
        Task<HttpResponseMessage> GetResponseFor(Exception exception, ILoggingContextScope loggingScope);
    }
}
