// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.ApplicationInsights.Configuration;

namespace Tiny.Framework.Web.ApplicationInsights.Registration
{
    /// <summary>
    /// the monitoring registration routines.
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "delegate mechanism")]
    public static class MonitoringRegistration
    {
        /// <summary>
        /// the add monitoring routine.
        /// </summary>
        public const string AddMonitoringRoutine = nameof(AddMonitoring);

        /// <summary>
        /// add swagger services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddMonitoring(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<MonitoringConfiguration>(MonitoringConfiguration.AppSettingsKey);

            ColorConsole.Startup($"adding application insights with key: '{service.InstrumentationKey}'");

            services.AddApplicationInsightsTelemetry(service.InstrumentationKey);
        }
    }
}