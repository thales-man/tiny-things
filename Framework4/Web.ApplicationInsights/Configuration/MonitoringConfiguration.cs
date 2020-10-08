// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Registration;

namespace Tiny.Framework.Web.ApplicationInsights.Configuration
{
    /// <summary>
    /// monitoring configuration.
    /// </summary>
    internal sealed class MonitoringConfiguration :
        IConfigureMonitoring,
        ISupportConfigurationRegistration
    {
        /// <summary>
        /// the app settings key.
        /// </summary>
        public const string AppSettingsKey = "SystemMonitoring";

        /// <inheritdoc/>
        public string InstrumentationKey { get; set; }

        /// <inheritdoc/>
        public string Environment { get; set; }

        /// <inheritdoc/>
        public string Component { get; set; }
    }
}