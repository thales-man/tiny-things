// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tiny.Framework.Providers;
using Tiny.Framework.Registration;
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.Factories;
using Tiny.Framework.Web.Faults;

namespace Tiny.Framework.Web.Providers
{
    /// <summary>
    /// the fault response provider (implementation).
    /// </summary>
    internal sealed class FaultResponseProvider :
        IProvideFaultResponses,
        ISupportServiceRegistration
    {
        private long _isLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaultResponseProvider"/> class.
        /// </summary>
        /// <param name="details">the registration details provider.</param>
        /// <param name="response">the http response message factory.</param>
        public FaultResponseProvider(
            IProvideRegistrationDetails details,
            ICreateHttpResponseMessages response)
        {
            It.IsNull(details)
                .AsGuard<ArgumentNullException>(nameof(details));
            It.IsNull(response)
                .AsGuard<ArgumentNullException>(nameof(response));

            Details = details;
            Response = response;

            Map.Add(typeof(FallbackActionException), x => GenericResponse(HttpStatusCode.BadRequest, x.Message));
        }

        /// <summary>
        /// gets or sets a value indicating whether is loaded.
        /// </summary>
        internal bool IsLoaded
        {
            get => Interlocked.Read(ref _isLoaded) == 1;
            set => Interlocked.Exchange(ref _isLoaded, value ? 1 : 0);
        }

        /// <summary>
        /// gets the exception map.
        /// </summary>
        internal ExceptionMaps Map { get; } = new ExceptionMaps();

        /// <summary>
        /// gets the registration provider details.
        /// </summary>
        internal IProvideRegistrationDetails Details { get; }

        /// <summary>
        /// gets the http response message factory.
        /// </summary>
        internal ICreateHttpResponseMessages Response { get; }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetResponseFor(Exception exception, ILoggingContextScope loggingScope)
        {
            if (!IsLoaded)
            {
                await Load();
            }

            var exceptionType = exception.GetType();

            if (Map.ContainsKey(exceptionType))
            {
                await InformOn(exception, loggingScope);
                var respondWith = Map[exceptionType];

                return respondWith(exception);
            }

            await loggingScope.Log(exception);

            var fallbackTo = Map[typeof(FallbackActionException)];

            return fallbackTo(exception);
        }

        /// <summary>
        /// Load...
        /// </summary>
        /// <returns>the currently running task.</returns>
        internal async Task Load()
        {
            IsLoaded = true;
            var cache = await Details.Load();
            cache.ForEach(LoadRegistrations);
        }

        /// <summary>
        /// Load (the) registrations...
        /// </summary>
        /// <param name="registrant">(for the) registrant.</param>
        internal void LoadRegistrations(Assembly registrant)
        {
            It.IsNull(registrant)
                .AsGuard<ArgumentNullException>(nameof(registrant));

            Details.GetAttributeListFor<FaultResponseRegistrationAttribute>(registrant)
                .ForEach(registration =>
                {
                    Map.Add(registration.ExceptionType, x => GenericResponse(registration.ResponseCode, x.Message));
                });
        }

        /// <summary>
        /// Inform on...
        /// </summary>
        /// <param name="exception">the exception.</param>
        /// <param name="loggingScope">the logging scope.</param>
        /// <returns>the currently running task.</returns>
        internal async Task InformOn(Exception exception, ILoggingContextScope loggingScope)
        {
            await loggingScope.Log(exception.Message);

            if (It.Has(exception.InnerException))
            {
                await InformOn(exception.InnerException, loggingScope);
            }
        }

        /// <summary>
        /// A generic registration response routine.
        /// </summary>
        /// <param name="code">the message return code.</param>
        /// <param name="message">the (optional) message.</param>
        /// <returns>A http response message.</returns>
        internal HttpResponseMessage GenericResponse(HttpStatusCode code, string message = "") =>
            Response.Create(code, message);
    }
}