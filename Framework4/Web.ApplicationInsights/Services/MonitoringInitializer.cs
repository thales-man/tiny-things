// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Tiny.Framework.Registration;
using Tiny.Framework.Web.ApplicationInsights.Configuration;

namespace Tiny.Framework.Web.ApplicationInsights.Services
{
    /// <summary>
    /// application insights initialisation.
    /// </summary>
    public class MonitoringInitializer :
        ITelemetryInitializer,
        ISupportServiceRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringInitializer"/> class.
        /// </summary>
        /// <param name="configuration">the configuration.</param>
        public MonitoringInitializer(IConfigureMonitoring configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// gets the configuration.
        /// </summary>
        internal IConfigureMonitoring Configuration { get; }

        /// <inheritdoc/>
        public void Initialize(ITelemetry telemetry)
        {
            AssignGlobalProperty(telemetry, "environment", Configuration.Environment);
            AssignGlobalProperty(telemetry, "component", Configuration.Component);
        }

        /// <summary>
        /// assigns a global property to hte telemetry.
        /// </summary>
        /// <param name="telemetry">the telemetry.</param>
        /// <param name="keyName">the key name.</param>
        /// <param name="keyValue">the key value.</param>
        internal void AssignGlobalProperty(ITelemetry telemetry, string keyName, string keyValue) =>
            telemetry.Context.GlobalProperties[keyName] = keyValue;
    }
}