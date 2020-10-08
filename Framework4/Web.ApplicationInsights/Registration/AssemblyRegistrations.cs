// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Microsoft.ApplicationInsights.Extensibility;
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Web.ApplicationInsights.Configuration;
using Tiny.Framework.Web.ApplicationInsights.Registration;
using Tiny.Framework.Web.ApplicationInsights.Services;

// Project level
// Services
[assembly: InternalRegistration(typeof(ITelemetryInitializer), typeof(MonitoringInitializer), TypeOfRegistrationScope.Singleton)]

// Configuration
[assembly: ConfigurationRegistration(typeof(IConfigureMonitoring), typeof(MonitoringConfiguration), MonitoringConfiguration.AppSettingsKey)]

// Build Routines
[assembly: AddRoutineRegistration(typeof(MonitoringRegistration), MonitoringRegistration.AddMonitoringRoutine)]
