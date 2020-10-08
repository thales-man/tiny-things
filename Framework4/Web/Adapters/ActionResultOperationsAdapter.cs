// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiny.Framework.Flow;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.Factories;
using Tiny.Framework.Web.Helpers;
using Tiny.Framework.Web.Providers;

namespace Tiny.Framework.Web.Adapters
{
    /// <summary>
    /// Action result operations adapter (implementation).
    /// </summary>
    internal sealed class ActionResultOperationsAdapter :
        IAdaptActionResultOperations,
        ISupportServiceRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultOperationsAdapter"/> class.
        /// </summary>
        /// <param name="safeOperations">the safe operations provider.</param>
        /// <param name="responseProvider">the fault response provider.</param>
        /// <param name="loggingFactory">the logging context factory.</param>
        public ActionResultOperationsAdapter(
            IProvideSafeOperations safeOperations,
            IProvideFaultResponses responseProvider,
            ICreateLoggingContexts loggingFactory)
        {
            It.IsNull(safeOperations)
                .AsGuard<ArgumentNullException>(nameof(safeOperations));
            It.IsNull(responseProvider)
                .AsGuard<ArgumentNullException>(nameof(responseProvider));
            It.IsNull(loggingFactory)
                .AsGuard<ArgumentNullException>(nameof(loggingFactory));

            Operation = safeOperations;
            Faulted = responseProvider;
            Logging = loggingFactory;
        }

        /// <summary>
        /// gets the (safe) operation (provider).
        /// </summary>
        internal IProvideSafeOperations Operation { get; }

        /// <summary>
        /// gets the fault (response provider).
        /// </summary>
        internal IProvideFaultResponses Faulted { get; }

        /// <summary>
        /// gets the logging (context factory).
        /// </summary>
        internal ICreateLoggingContexts Logging { get; }

        /// <inheritdoc/>
        public async Task<IActionResult> Run(Func<Task<HttpResponseMessage>> coordinatedAction, string traceMessage)
        {
            using (var loggingScope = Logging.BeginLoggingFor(traceMessage))
            {
                return await Operation.Try(coordinatedAction, x => ProcessError(x, loggingScope))
                    .AsActionResult();
            }
        }

        /// <summary>
        /// Process error...
        /// </summary>
        /// <param name="exception">the raised exception.</param>
        /// <param name="loggingContext">the logging context.</param>
        /// <returns>the message response and status code.</returns>
        internal async Task<HttpResponseMessage> ProcessError(Exception exception, ILoggingContextScope loggingContext) =>
            await Faulted.GetResponseFor(exception, loggingContext);
    }
}
