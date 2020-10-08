// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Web.Swashbuckle.Registration;

// Project level
// Build Routines
[assembly: AddRoutineRegistration(typeof(SwaggerRegistration), SwaggerRegistration.AddSwaggerRoutine)]
[assembly: UseRoutineRegistration(typeof(SwaggerRegistration), SwaggerRegistration.UseSwaggerRoutine)]
