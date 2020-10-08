// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Web.Shell.Registration;

// Project level
// Build Routines
[assembly: AddRoutineRegistration(typeof(StartupRegistration), StartupRegistration.AddCoreMvcRoutine)]
[assembly: AddRoutineRegistration(typeof(StartupRegistration), StartupRegistration.AddCorsRoutine)]
[assembly: AddRoutineRegistration(typeof(StartupRegistration), StartupRegistration.AddAuthorizationRoutine)]
[assembly: AddRoutineRegistration(typeof(StartupRegistration), StartupRegistration.AddControllersAsServicesRoutine)]
[assembly: AddRoutineRegistration(typeof(StartupRegistration), StartupRegistration.AddSlugifyTransformationRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseForwardedHeadersRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseRoutingRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseCorsRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseHttpsRedirectionRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseAuthenticationRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseAuthorizationRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseStaticFilesRoutine)]
[assembly: UseRoutineRegistration(typeof(StartupRegistration), StartupRegistration.UseBasicDeploymentRoutine)]
