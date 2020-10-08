// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Net;
using Tiny.Framework.Flow;
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Web.Adapters;
using Tiny.Framework.Web.Factories;
using Tiny.Framework.Web.Faults;
using Tiny.Framework.Web.Providers;

// Project level
// Adapters
[assembly: InternalRegistration(typeof(IAdaptActionResultOperations), typeof(ActionResultOperationsAdapter), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(IDeviceAdapter), typeof(DeviceAdapter), TypeOfRegistrationScope.Singleton)]

// Factories
[assembly: InternalRegistration(typeof(ICreateLoggingContexts), typeof(LoggingContextFactory), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(ICreateHttpResponseMessages), typeof(HttpResponseMessageFactory), TypeOfRegistrationScope.Singleton)]

// Providers
[assembly: InternalRegistration(typeof(IProvideFaultResponses), typeof(FaultResponseProvider), TypeOfRegistrationScope.Singleton)]

// Faults
[assembly: FaultResponseRegistration(typeof(ConflictingResourceException), HttpStatusCode.Conflict)]
[assembly: FaultResponseRegistration(typeof(MalformedRequestException), HttpStatusCode.BadRequest)]
[assembly: FaultResponseRegistration(typeof(NoContentException), HttpStatusCode.NoContent)]
[assembly: FaultResponseRegistration(typeof(UnprocessableEntityException), (HttpStatusCode)422)]
