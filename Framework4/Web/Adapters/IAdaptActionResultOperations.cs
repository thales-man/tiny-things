// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tiny.Framework.Web.Adapters
{
    /// <summary>
    /// I adapt action result operations (contract).
    /// </summary>
    public interface IAdaptActionResultOperations
    {
        /// <summary>
        /// Run...
        /// </summary>
        /// <param name="coordinatedAction">the coordinated action.</param>
        /// <param name="traceMessage">the opening trace message.</param>
        /// <returns>the result of the action.</returns>
        Task<IActionResult> Run(Func<Task<HttpResponseMessage>> coordinatedAction, string traceMessage);
    }
}
